using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        int randomLane = Random.Range(0, lanes.Length);

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
        //Debug.Log(">>><<<><>><><");
        //Gamestuff here
        SetUpGamePlay();

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
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


        //StopCoroutine(StartBoxingGame());
    }

    private IEnumerator StartSwimmingGame()
    {
        countDownGraphic.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        countDownGraphic.SetActive(false);
        AudioManager.Instance.PlaySound("boxing_ding");
       

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
        }
       
        if(finishOverlays.Count >= players.Count)
        {
            ShowResults();
        }

        CameraManager.Instance.SetLive(stateCam);
        
    }



    public void ShowResults()
    {
        Highscores.Instance.AddNewHighscore(playerScore.ToString(), 2);
        Highscores.Instance.DownloadHighscores(2);
        ToggleActivityUIForResults(playerScore.ToString());
        AudioManager.Instance.PlaySound("boxing_cheer");
    }

}
