using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingStateAction : ActivityStateChange, IInteractable
{
    public void Deselect()
    {
        activityCanvas.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if(GameManager.Instance.GameState == GameStates.Activity)
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
        activityCanvas.gameObject.SetActive(true);
    }

}
