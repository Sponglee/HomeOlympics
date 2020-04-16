﻿using UnityEngine;
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

    public class HighlightAllEvent : UnityEvent { }
    public static HighlightAllEvent OnHighLightAll = new HighlightAllEvent();

    public Transform targetedActivity = null;
    public GameStates lastState;

    
    [SerializeField] private GameStates gameState;
    [SerializeField] private Transform selectionCanvas;

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
        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnHighLightAll.Invoke();
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            OnHighLightAll.Invoke();
        }
    }



    public void SelectActivity(Transform target, string name)
    {
        targetedActivity = target;
        Debug.Log(name);
        selectionCanvas.gameObject.SetActive(true);
        selectionCanvas.position = target.position;
        selectionCanvas.GetComponent<ActivitySelectionUI>().UpdateSelectionText(name);
    }

    public void DeselectActivity()
    {
        selectionCanvas.gameObject.SetActive(false);
    }
}
