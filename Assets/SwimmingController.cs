using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SwimmingController : ActivityControllerBase
{

    public class SwimmingGameStarted : UnityEvent { }
    public static SwimmingGameStarted OnSwimmingGameStarted = new SwimmingGameStarted();

    public class PlayerFinishedEvent : UnityEvent<float, LaneController> { }
    public static PlayerFinishedEvent onFinish = new PlayerFinishedEvent();

    public LaneController[] lanes;
    public GameObject playerPref;
    public GameObject opponentPref;

    public Transform playersHolder;

    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera stateCam;
    public GameObject countDownGraphic;

    public List<SwimmingCanvasOverlayController> finishOverlays = new List<SwimmingCanvasOverlayController>();

    private void Start()
    {
        onFinish.AddListener(SwimmingFinished);
    }


    public override void DeInitializeActivity()
    {
        base.DeInitializeActivity();
        KillGamePlay();
    }

    public override void InitializeActivity()
    {
        base.InitializeActivity();
        InitializeGamePlay();
    }

    public void SetUpGamePlay()
    {
        int randomLane = Random.Range(0, lanes.Length);

        GameObject tmpPlayer = Instantiate(playerPref, lanes[randomLane].startPoint.position, lanes[randomLane].startPoint.rotation, lanes[randomLane].transform);
        gameCam.Follow = tmpPlayer.transform;
        gameCam.LookAt = tmpPlayer.transform;

        for (int i = 0; i < lanes.Length; i++)
        {
            if (i != randomLane)
            {
                GameObject tmpOpponent = Instantiate(opponentPref, lanes[i].startPoint.position, lanes[i].startPoint.rotation, lanes[i].transform);
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
        foreach (Transform item in playersHolder)
        {
            Destroy(item.gameObject);
        }

        OnSwimmingGameStarted.RemoveAllListeners();
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

    private LaneController winner;

    public void SwimmingFinished(float time, LaneController target)
    {
      

        if(finishOverlays.Count == 0)
        {
            winner = target;
        }

        if(target.CompareTag("Player"))
        {
        
        }
       

        CameraManager.Instance.SetLive(stateCam);
        winner.overlay.gameObject.SetActive(true);
    }

}
