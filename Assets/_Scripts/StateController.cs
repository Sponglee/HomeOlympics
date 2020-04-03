using UnityEngine;
using UnityEngine.Events;

public enum GameStates
{
    Walking,
    Activity,
    Result,
    Paused
}

public class StateController : Singleton<StateController>
{
    public class UpdateStateEvent : UnityEvent<GameStates> { }
    public static UpdateStateEvent UpdateState = new UpdateStateEvent();


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
            UpdateState.Invoke(value);
        }
    }


    [Header("")]
    [SerializeField] private GameStates gameState;
    [SerializeField] private int score = 0;
    private GameStates lastState;



   

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
