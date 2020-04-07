using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateChange : StateChangeBase, IInteractable
{
    public string activityName;
    public ActivityControllerBase activity;

    public override void StateChangeActionOff()
    {
        activity.DeactivateActivity();
    }

    public override void StateChangeActionOn()
    {
        activity.InitializeActivity();
    }

    public void Interact()
    {
      
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


    public void Select()
    {
        GameManager.Instance.SelectActivity(transform, activityName);
    }

    public void Deselect()
    {
        GameManager.Instance.DeselectActivity();
    }
}
