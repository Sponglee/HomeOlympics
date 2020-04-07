using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    Walking,
    Activity,
    Result,
    Paused
}

public class GameManager : Singleton<GameManager>
{
    public class UpdateStateEvent : UnityEvent<GameStates,Transform> { }
    public static UpdateStateEvent UpdateState = new UpdateStateEvent();

    [SerializeField] private Transform targetedActivity = null;
    [SerializeField] private GameStates gameState;
    [SerializeField] private Canvas activityCanvas;




    public GameStates lastState;
    public GameStates GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            if (value != gameState)
            {
                lastState = gameState;
            }
            gameState = value;
            UpdateState.Invoke(value, targetedActivity);
            Debug.Log(value);
        }
    }
    //[Header("")]
    


   

    //public class UpdateScoreEvent : UnityEvent<int> { }
    //public static UpdateScoreEvent UpdateScore = new UpdateScoreEvent();


    private void Start()
    {
        GameState = GameStates.Paused;
    }


    //Debug
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameState != GameStates.Paused)
            {
                GameState = GameStates.Paused;
            }
            else
                GameState = lastState;
        }
    }



    public void SelectActivity(Transform target, string name)
    {
        targetedActivity.position = transform.position;
        Debug.Log(name);
        activityCanvas.gameObject.SetActive(true);
        activityCanvas.transform.position = target.position;
        
    }

    public void DeselectActivity()
    {
        activityCanvas.gameObject.SetActive(false);
    }
}
