using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateChange : StateChangeBase, IInteractable
{
    public Canvas activityCanvas;
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
        GameManager.Instance.targetedActivity = transform;
        Debug.Log(gameObject.name);
        activityCanvas.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        //Debug.Log(">D : " + gameObject.name);
        activityCanvas.gameObject.SetActive(false);
        GameManager.Instance.targetedActivity = null;
    }
}
