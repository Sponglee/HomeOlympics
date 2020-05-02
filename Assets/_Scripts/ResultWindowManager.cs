﻿using UnityEngine;
using TMPro;
using UnityEngine.Events;


public struct ActivityResultInfo
{
    public string ActivityName;
    public string ActivityScore;
}


public class ResultWindowManager : Singleton<ResultWindowManager>
{
    public class ResultsWindowOpenEvent : UnityEvent<string, string, int> { }
    public static ResultsWindowOpenEvent OnResultsOpened = new ResultsWindowOpenEvent();

    public Transform canvasHolder;
    public TextMeshProUGUI activityResultName;
    public GameObject resultRowPref;
    public Transform resultsContainer;
    public ResultRowController currentPlayerRow;


    private void Start()
    {
        OnResultsOpened.AddListener(UpdateCurrentResultWindow);
    }

    private void UpdateCurrentResultWindow(string score, string name, int decimals = 0)
    {
        Debug.Log(score + " : " + name);
        int medalColorIndex = 0;
        activityResultName.text = name;
        currentPlayerRow.UpdateRowInfo(PlayerInfoManager.Instance.playerName, score, PlayerInfoManager.Instance.playerFlag, PlayerInfoManager.Instance.medalColors[medalColorIndex], decimals);

      
    }


    public void OpenResultWindow()
    {
        FunctionHandler.Instance.ToggleBaseUI();
        canvasHolder.gameObject.SetActive(true);
    }

    public void CloseResultWindow()
    {
        canvasHolder.gameObject.SetActive(false);
       
    }

}
