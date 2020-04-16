using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateChange : StateChangeBase, IInteractable
{
    public string activityName;
    public ActivityControllerBase activity;
   
    public override void StateChangeActionOff()
    {
        if(GameManager.Instance.targetedActivity == this.transform)
            activity.DeactivateActivity();
    }

    public override void StateChangeActionOn()
    {
        activity.ActivateActivity();
    }

    public void Interact()
    {
        Debug.Log("INTERACT");
        if (GameManager.Instance.GameState == GameStates.Activity)
        {
            GameManager.Instance.GameState = GameStates.Walking;
            Select();
        }
        else
        {
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
