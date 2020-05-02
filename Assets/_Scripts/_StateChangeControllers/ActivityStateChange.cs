using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateChange : StateChangeBase, IInteractable
{
    public string activityName;
    public ActivityControllerBase activity;
   
    public override void StateChangeActionOff()
    {

        if (GameManager.Instance.targetedActivity == this.transform)
        {
            activity.DeactivateActivity();
            activity.enabled = false;
        }
    }

    public override void StateChangeActionOn()
    {
        activity.enabled = true;
        activity.ActivateActivity();
    }

    public void Interact()
    {
        if (GameManager.Instance.GameState == GameStates.Activity)
        {
            //Debug.Log("Select");
            GameManager.Instance.GameState = GameStates.Walking;
            if(ResultWindowManager.Instance.canvasHolder.gameObject.activeSelf)
            {
                ResultWindowManager.Instance.CloseResultWindow();
            }
            Select();
        }
        else
        {
            //Debug.Log("Deselect");
            GameManager.Instance.GameState = GameStates.Activity;
            Deselect();
        }
    }

    private void Start()
    {
        GameManager.OnHighLightAll.AddListener(HighlightActivity);
    }

    public void HighlightActivity()
    {
            transform.GetComponent<QuickOutline>().enabled = !transform.GetComponent<QuickOutline>().isActiveAndEnabled;
    }

    public void Select()
    {
        GameManager.Instance.SelectActivity(transform, activityName);
        HighlightActivity();
    }

    public void Deselect()
    {
       
        GameManager.Instance.DeselectActivity();
        HighlightActivity();
    }


}
