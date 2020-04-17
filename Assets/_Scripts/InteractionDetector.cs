using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.GameState != GameStates.Activity && other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Select();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.GameState != GameStates.Activity && other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Deselect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("INTERACTED >>" + other.gameObject.name);
            if (other.GetComponent<IInteractable>() != null)
            {
                other.GetComponent<IInteractable>().Interact();
            }
        }
    }
}
