using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasketBallController : ActivityControllerBase
{
    public class BasketDunkedEvent : UnityEvent { }
    public static BasketDunkedEvent OnBasketDunked = new BasketDunkedEvent();

    public class BasketScoreUpdateEvent : UnityEvent<string> { }
    public static BasketScoreUpdateEvent OnBasketScoreChanged = new BasketScoreUpdateEvent();

    public class BasketTimerUpdateEvent : UnityEvent<int> { }
    public static BasketTimerUpdateEvent OnBasketTimerChanged = new BasketTimerUpdateEvent();

    public int BasketScores
    {
        get
        {
            return basketScores;
        }

        set
        {
            basketScores = value;
            OnBasketScoreChanged.Invoke(value.ToString());
        }
    }

    public int BasketTimer
    {
        get
        {
            return basketTimer;
        }

        set
        {
            basketTimer = value;
            if (value >= 0)
            {
                OnBasketTimerChanged.Invoke(value);
            }
            else
            {
                GameActive = false;
                Highscores.Instance.AddNewHighscore(basketScores.ToString(), 3);
                Highscores.Instance.DownloadHighscores(3);
                ToggleActivityUIForResults(BasketScores.ToString());
                AudioManager.Instance.PlaySound("cheer");
            }
        }
    }

    public GameObject CurrentRoll
    {
        get
        {
            return currentRoll;
        }

        set
        {
            currentRoll = value;
            if (value == null)
            {
                SpawnRoll();
            }
        }
    }

    public GameObject countDownGraphic;
    public Vector2 speedRange = new Vector2(2f, 30f);
    public float currentSpeed = 0f;

    [SerializeField] private bool GameActive = false;

    [SerializeField] private int basketTimer;
    [SerializeField] private int basketScores = 0;

    [SerializeField] private GameObject basketHide;
    [SerializeField] private GameObject basketContent;
    
    [SerializeField] private CinemachineVirtualCamera vcamGame;
    [SerializeField] private CinemachineVirtualCamera vCamState;

    [SerializeField] private GameObject toiletPaperPref;

    [SerializeField] private BasketBallInputController handPoint;
    [SerializeField] private Transform tpHolder;

    

    [SerializeField] private float speedRate = 1f;
    [SerializeField] private GameObject currentRoll = null;

    [SerializeField] private GameObject basketInputHolder;
    [SerializeField] private Transform basketPoints;
    [SerializeField] private Transform basket;

   

    public override void DeInitializeActivity()
    {
        KillGame();
        basketInputHolder.SetActive(false);
        basketContent.SetActive(false);
        basketHide.SetActive(true);
        base.DeInitializeActivity();
    }

    public override void InitializeActivity()
    {
        basketInputHolder.SetActive(false);
        basketContent.SetActive(true);
        basketHide.SetActive(false);
        base.InitializeActivity();
        InitializeGamePlay();
    }

    private void InitializeGamePlay()
    {
        //Debug.Log(">>><<<><>><><");
        //Gamestuff here
        
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartBasketGame());
    }

    private void Start()
    {
        OnBasketDunked.AddListener(DunkedHandler);
    }

    private void Update()
    {
        if(GameActive)
        {
            if (Input.GetMouseButton(0))
            {
                currentSpeed += speedRate;
                currentSpeed = Mathf.Clamp(currentSpeed, speedRange.x, speedRange.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {

                if (CurrentRoll != null)
                {
                    CurrentRoll.transform.SetParent(tpHolder);
                    CurrentRoll.GetComponent<Rigidbody>().isKinematic = false;
                    CurrentRoll.GetComponent<Rigidbody>().AddForce(CurrentRoll.transform.forward * currentSpeed);
                    CurrentRoll = null;

                    currentSpeed = speedRange.x;
                }


            }
        }
        
    }

    public void SpawnRoll()
    {
        CurrentRoll = Instantiate(toiletPaperPref, handPoint.transform);
        
    }



    private IEnumerator StartBasketGame()
    {
        countDownGraphic.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        countDownGraphic.SetActive(false);
        AudioManager.Instance.PlaySound("boxing_ding");

        GameActive = true;

        Cursor.visible = false;
        StartGame();

        while (GameActive)
        {
            yield return new WaitForSecondsRealtime(1f);
            BasketTimeCountdown();

        }
    }

    private void BasketTimeCountdown()
    {
        BasketTimer--;
    }

    private void KillGame()
    {
        GameActive = false;
        BasketScores = 0;
        StopAllCoroutines();
        
        foreach (Transform item in tpHolder)
        {
            Destroy(item.gameObject);
        }
    }

    private void StartGame()
    {
        basketInputHolder.SetActive(true);
        CameraManager.Instance.SetLive(vcamGame);
        CurrentRoll = Instantiate(toiletPaperPref, handPoint.transform);

        SpawnRandomBasket();
        //OnSwimmingGameStarted.Invoke();
    }


    private void DunkedHandler()
    {
        BasketScores++;
        SpawnRandomBasket();
    }

    private void SpawnRandomBasket()
    {
        basket.position = basketPoints.GetChild(Random.Range(0, basketPoints.childCount)).position;
    }



}
