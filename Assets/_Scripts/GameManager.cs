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

    public Transform targetedActivity = null;

    public GameStates GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            Debug.Log(value);
            if (value != gameState)
            {
                lastState = gameState;
            }
            gameState = value;
            UpdateState.Invoke(value, targetedActivity);
        }
    }


    //[Header("")]
    public GameStates lastState;
    [SerializeField] private GameStates gameState;
    


   

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

}
