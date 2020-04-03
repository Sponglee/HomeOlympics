using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityStateChange : StateChangeBase, IInteractable
{
    public Canvas activityCanvas;

    public override void StateChangeActionOff()
    {
        FunctionHandler.Instance.uiCanvas.gameObject.SetActive(false);
    }

    public override void StateChangeActionOn()
    {
        FunctionHandler.Instance.uiCanvas.gameObject.SetActive(true);
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
        Debug.Log(">D : " + gameObject.name);
        activityCanvas.gameObject.SetActive(false);
        GameManager.Instance.targetedActivity = null;
    }
}
