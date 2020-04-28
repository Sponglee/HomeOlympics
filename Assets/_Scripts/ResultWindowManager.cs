using UnityEngine;
using TMPro;
using UnityEngine.Events;


public struct ActivityResultInfo
{
    public string ActivityName;
    public string ActivityScore;
}


public class ResultWindowManager : Singleton<ResultWindowManager>
{
    public class ResultsWindowOpenEvent : UnityEvent<ActivityResultInfo> { }
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

    private void UpdateCurrentResultWindow(ActivityResultInfo resultInfo)
    {
        Debug.Log(resultInfo.ActivityName + " : " + resultInfo.ActivityScore);
        int medalColorIndex = 0;
        activityResultName.text = resultInfo.ActivityName;
        currentPlayerRow.UpdateRowInfo(PlayerInfoManager.Instance.playerName, resultInfo.ActivityScore, PlayerInfoManager.Instance.playerFlag, PlayerInfoManager.Instance.medalColors[medalColorIndex]);
    }


    public void OpenResultWindow()
    {
        canvasHolder.gameObject.SetActive(true);
    }

    public void CloseResultWindow()
    {
        canvasHolder.gameObject.SetActive(false);
    }

}
