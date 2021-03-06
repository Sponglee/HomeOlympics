﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;


public class SwimmingController : ActivityControllerBase
{
    

    public class SwimmingGameStarted : UnityEvent { }
    public static SwimmingGameStarted OnSwimmingGameStarted = new SwimmingGameStarted();

    public class PlayerFinishedEvent : UnityEvent<float, LaneController, float> { }
    public static PlayerFinishedEvent onFinish = new PlayerFinishedEvent();

    public LaneController[] lanes;
    public GameObject playerPref;
    public GameObject opponentPref;

    public List<GameObject> players = new List<GameObject>();

    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera stateCam;
    public GameObject countDownGraphic;

    public GameObject swimmingContent;
    public GameObject swimmingHide;

    public List<SwimmingCanvasOverlayController> finishOverlays = new List<SwimmingCanvasOverlayController>();

    private LaneController winner;
    private float playerScore;

    private void Start()
    {
        onFinish.AddListener(SwimmingFinished);
    }


    public override void DeInitializeActivity()
    {
        swimmingContent.SetActive(false);
        swimmingHide.SetActive(true);
        base.DeInitializeActivity();
        KillGamePlay();
        StopAllCoroutines();
        CancelInvoke();
    
    }

    public override void InitializeActivity()
    {
        swimmingContent.SetActive(true);
        swimmingHide.SetActive(false);
        base.InitializeActivity();
        InitializeGamePlay();
    }

    public void SetUpGamePlay()
    {
        int randomLane = Random.Range(1, lanes.Length);

        GameObject tmpPlayer = Instantiate(playerPref, lanes[randomLane].startPoint.position, lanes[randomLane].startPoint.rotation, lanes[randomLane].transform);
        gameCam.Follow = tmpPlayer.transform;
        gameCam.LookAt = tmpPlayer.transform;
        lanes[randomLane].swimmer = tmpPlayer.transform;
        players.Add(tmpPlayer);

        for (int i = 0; i < lanes.Length; i++)
        {
            if (i != randomLane)
            {
                GameObject tmpOpponent = Instantiate(opponentPref, lanes[i].startPoint.position, lanes[i].startPoint.rotation, lanes[i].transform);
                lanes[i].swimmer = tmpOpponent.transform;
                players.Add(tmpOpponent);
            }
        }

    }


    private void InitializeGamePlay()
    {
        //Gamestuff here
        SetUpGamePlay();
        StartCoroutine(StartSwimmingGame());
    }

    private void KillGamePlay()
    {
        StopAllCoroutines();

        foreach (GameObject item in players)
        {
            Destroy(item);
        }

        players.Clear();

        OnSwimmingGameStarted.RemoveAllListeners();

        foreach (SwimmingCanvasOverlayController controller in finishOverlays)
        {
            controller.gameObject.SetActive(false);
        }

        finishOverlays.Clear();
    }

    private IEnumerator StartSwimmingGame()
    {
        countDownGraphic.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        countDownGraphic.SetActive(false);
        AudioManager.Instance.PlaySound("swimming_beep");
    
        StartGame();
    }


    private void StartGame()
    {
        CameraManager.Instance.SetLive(gameCam);
        OnSwimmingGameStarted.Invoke();
    }

 

    public void SwimmingFinished(float time, LaneController target, float result)
    {
        if(finishOverlays.Count == 0)
        {
            winner = target;
        }

        SwimmingCanvasOverlayController tmpOverlay = target.overlay.GetComponent<SwimmingCanvasOverlayController>();
        finishOverlays.Add(tmpOverlay);
        tmpOverlay.gameObject.SetActive(true);
        tmpOverlay.ShowGraphic(finishOverlays.IndexOf(tmpOverlay),target.swimmer.GetComponent<SwimmingSwimmer>().flag);

        if (target.swimmer.transform.CompareTag("Player"))
        {
            playerScore = result;
            Invoke(nameof(ShowResults), 3f);
        }

        CameraManager.Instance.SetLive(stateCam);
    }

    public void ShowResults()
    {
        Highscores.Instance.AddNewHighscore((-playerScore*100f).ToString(),2);
        Highscores.Instance.DownloadHighscores(2,2);
        ToggleActivityUIForResults((playerScore*100f).ToString(),2);
        StopBackSound();
        AudioManager.Instance.PlaySound("cheer");
    }

    public override void ToggleActivityUIForResults(string score, int decimals)
    {
        activityUI.SetActive(false);

        string activity = transform.GetComponent<ActivityStateChange>().activityName;
        string scores = score.ToString();

        if (!GameManager.Instance.resultsCanvas.activeSelf)
        {
            ResultWindowManager.Instance.OpenResultWindow();
            Debug.Log(activity + " = " + score);
            StartCoroutine(ResultsInvokeDelay(scores, activity,2));
        }
    }
}
