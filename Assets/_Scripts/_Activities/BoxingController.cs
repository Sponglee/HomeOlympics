using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxingController : ActivityControllerBase
{
    public class BoxingTargetDestroyedEvent : UnityEvent<GameObject> { }
    public static BoxingTargetDestroyedEvent TargetDestroyed = new BoxingTargetDestroyedEvent();

    public class BoxingScoreUpdateEvent : UnityEvent<string> { }
    public static BoxingScoreUpdateEvent OnBoxingScoreChanged = new BoxingScoreUpdateEvent();

    public class BoxingTimerUpdateEvent : UnityEvent<int> { }
    public static BoxingTimerUpdateEvent OnBoxingTimerChanged = new BoxingTimerUpdateEvent();

    [SerializeField] private GameObject boxingTargetPref;
    [SerializeField] private Transform boxingTargetLocations;
    [SerializeField] private Canvas boxingTargetsCanvas;
    [SerializeField] private Transform boxingTargetsHolder;
    [SerializeField] private List<GameObject> boxingTargets = new List<GameObject>(); 
    [SerializeField] private GameObject boxingHands;
    [SerializeField] private HookController hookController;

    //GamePlay
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
            OnBoxingTimerChanged.Invoke(value);
        }
    }

    private void Start()
    {
        TargetDestroyed.AddListener(TargetDestroyedHandler);    
    }


    public override void InitializeActivity()
    {
        Debug.Log(">"+gameObject.name);
        ToggleSystems();
        SpawnTargets();
    }

    public override void DeInitializeActivity()
    {
        Debug.Log("<"+gameObject.name);
        ToggleSystems();
    }


    public void ToggleSystems()
    {
        boxingTargetsCanvas.gameObject.SetActive(!boxingTargetsCanvas.gameObject.activeSelf);
        boxingHands.SetActive(!boxingHands.activeSelf);
       
        //Gamestuff here
        ResetGamePlay();
    }

    public void ResetGamePlay()
    {
        GameTimer = 60;
        BoxingScores = 0;

        foreach (Transform child in boxingTargetsHolder)
        {
            Destroy(child.gameObject);

        }
        boxingTargets.Clear();

    }

    public void SpawnTargets()
    {
        
        for (int i = 0; i < 3; i++)
        {
            GameObject tmpTarget = Instantiate(boxingTargetPref
                                                , boxingTargetLocations.GetChild(Random.Range(0,boxingTargetLocations.childCount)).position
                                                , Quaternion.LookRotation(Camera.main.transform.position - transform.position,Vector3.up)
                                                , boxingTargetsHolder);
            boxingTargets.Add(tmpTarget);
        }
    }


    private void TargetDestroyedHandler(GameObject target)
    {
        boxingTargets.Remove(target);
        Destroy(target);
        BoxingScores++;
        if(boxingTargets.Count<=0)
        {
            Invoke(nameof(SpawnTargets),0.2f);
        }
    }
}
