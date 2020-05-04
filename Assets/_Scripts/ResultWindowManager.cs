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
        //int medalColorIndex = 0;
        activityResultName.text = name;
        currentPlayerRow.UpdateRowInfo(GameManager.Instance.playerName, score, GameManager.Instance.playerFlag ,Color.white, decimals);

      
    }


    public void OpenResultWindow()
    {
        FunctionHandler.Instance.ToggleBaseUI();
        foreach (Transform item in resultsContainer)
        {
            Destroy(item.gameObject);
        }
        canvasHolder.gameObject.SetActive(true);
    }

    public void CloseResultWindow()
    {
        canvasHolder.gameObject.SetActive(false);
       
    }

}
