using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStateAction : StateChangeActivity, IInteractable
{
    public void Deselect()
    {
        activityCanvas.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if(StateController.Instance.GameState == GameStates.Activity)
        {
            StateController.Instance.GameState = GameStates.Walking;
            Select();
        }
        else
        {
            StateController.Instance.GameState = GameStates.Activity;
            Deselect();
        }
    }


    public void Select()
    {
        activityCanvas.gameObject.SetActive(true);
    }

}
