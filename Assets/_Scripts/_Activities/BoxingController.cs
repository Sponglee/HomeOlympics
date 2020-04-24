using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxingController : ActivityControllerBase
{
    //public class BoxingTargetDestroyedEvent : UnityEvent<GameObject> { }
    //public static BoxingTargetDestroyedEvent TargetDestroyed = new BoxingTargetDestroyedEvent();

    public class BoxingScoreUpdateEvent : UnityEvent<string> { }
    public static BoxingScoreUpdateEvent OnBoxingScoreChanged = new BoxingScoreUpdateEvent();

    public class BoxingTimerUpdateEvent : UnityEvent<int> { }
    public static BoxingTimerUpdateEvent OnBoxingTimerChanged = new BoxingTimerUpdateEvent();

   
    public GameObject[] boxingTargetPrefs;
    [SerializeField] private Transform boxingTargetLocations;
    [SerializeField] private Canvas boxingTargetsCanvas;
    [SerializeField] private Transform boxingTargetsHolder;
    [SerializeField] private List<GameObject> boxingTargets = new List<GameObject>(); 
    [SerializeField] private GameObject boxingHands;
    [SerializeField] private HookController hookController;

    //GamePlay
    [SerializeField] private GameObject countDownGraphic;
    [SerializeField] private int roundDuration = 60;
    [SerializeField] private int gameTimer;
    [SerializeField] private int boxingScores = 0;

    public int BoxingScores
    {
        get
        {
            return boxingScores;
        }

        set
        {
            boxingScores = value;
            OnBoxingScoreChanged.Invoke(value.ToString());
        }
    }
    public int GameTimer
    {
        get
        {
            return gameTimer;
        }

        set
        {
            gameTimer = value;
            if (value >= 0)
            {
                OnBoxingTimerChanged.Invoke(value);
            }
            else
            {
                hookController.CanHook = false;

                OpenResults();
            }
        }
    }



    public override void InitializeActivity()
    {
        Debug.Log(">"+gameObject.name);
        boxingTargetsCanvas.gameObject.SetActive(true);
        boxingHands.SetActive(true);
        InitializeGamePlay();
        
    }

    public override void DeInitializeActivity()
    {
        Debug.Log("<"+gameObject.name);
        boxingTargetsCanvas.gameObject.SetActive(false);
        boxingHands.SetActive(false);
        KillGamePlay();
    }


 

    public void InitializeGamePlay()
    {
        Debug.Log(">>><<<><>><><");
        //Gamestuff here
        ResetGamePlay();

       
        StartCoroutine(StartBoxingGame());
    }

    public void KillGamePlay()
    {
        StopAllCoroutines();
        //StopCoroutine(StartBoxingGame());
    }

    private IEnumerator StartBoxingGame()
    {
        countDownGraphic.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        countDownGraphic.SetActive(false);
        AudioManager.Instance.PlaySound("boxing_ding");
        hookController.CanHook = true;

        SpawnTargets();


        while (hookController.CanHook)
        {
            yield return new WaitForSecondsRealtime(1f);
            BoxingTimeCountdown();

        }

        //Game Stopped   
   
        AudioManager.Instance.PlaySound("boxing_finish");
        

        //Results here
        //ToggleResultsWindow();
    }


    private void BoxingTimeCountdown()
    {
        GameTimer--;
    }

    public void ResetGamePlay()
    {
        GameTimer = 60;
        BoxingScores = 0;
        foreach (Transform childHolder in boxingTargetsHolder)
        {
            foreach (Transform child in childHolder)
            {
                Destroy(child.gameObject);
            }
        }
      
        boxingTargets.Clear();

        hookController.RetractAll();

    }

    public void SpawnTargets()
    {

        List<int> spawnIndexes = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int index;

            do
            {
                index = Random.Range(0, boxingTargetLocations.childCount);

            }
            while (spawnIndexes.Contains(index));

            spawnIndexes.Add(index);

            int spawnSide = Random.Range(0, boxingTargetPrefs.Length);
            //Debug.Log(spawnSide);
            Vector3 targetPosition = boxingTargetLocations.GetChild(index).position;
            GameObject tmpTarget = Instantiate(boxingTargetPrefs[spawnSide]
                                                , targetPosition
                                                , Quaternion.LookRotation(Camera.main.transform.position - transform.position, Vector3.up)
                                                , boxingTargetsHolder.GetChild(spawnSide));
            boxingTargets.Add(tmpTarget);

        }

        spawnIndexes.Clear();

            
        
    }


    public void TargetDestroyedHandler(GameObject target)
    {
       
        boxingTargets.Remove(target);
        Destroy(target);
        BoxingScores++;
        AudioManager.Instance.PlaySound("boxing_hit");
        if (boxingTargets.Count<=0)
        {
            Invoke(nameof(SpawnTargets),0.2f);
        }
    }


    public void MissedHitHandler(Transform hook)
    {
        AudioManager.Instance.PlaySound("boxing_miss");

    }
}
