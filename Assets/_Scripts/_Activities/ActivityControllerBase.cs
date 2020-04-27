using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityControllerBase : MonoBehaviour
{
    public GameObject activityUI;

    public void ActivateActivity()
    {
        InitializeActivity();

        FunctionHandler.Instance.ToggleUI(transform.GetComponent<ActivityStateChange>().activityName);
        if (activityUI != null)
            activityUI.SetActive(true);
    }
    public void DeactivateActivity()
    {
        DeInitializeActivity();

        FunctionHandler.Instance.ToggleUI("0");

        if (activityUI != null)
        {
            activityUI.SetActive(false);
        }
    }

    public virtual void DeInitializeActivity() { }
    public virtual void InitializeActivity() { }


    public void ToggleActivityUIForResults(string score)
    {
        activityUI.SetActive(false);

        ActivityResultInfo tmpInfo = new ActivityResultInfo();
        tmpInfo.ActivityName = transform.GetComponent<ActivityStateChange>().activityName;
        tmpInfo.ActivityScore = score;

        if (!GameManager.Instance.resultsCanvas.activeSelf)
        {
            ResultWindowManager.Instance.OpenResultWindow();
            Debug.Log(tmpInfo.ActivityName + " = " + tmpInfo.ActivityScore);
            StartCoroutine(ResultsInvokeDelay(tmpInfo));
        }

       
    }

    private IEnumerator ResultsInvokeDelay(ActivityResultInfo tmpInfo)
    {
        yield return new WaitForSecondsRealtime(0.2f);
        ResultWindowManager.OnResultsOpened.Invoke(tmpInfo);
    }
}
