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


    public void OpenResults()
    {
        activityUI.SetActive(false);
        GameManager.Instance.ToggleResultSequence();
    }
}
