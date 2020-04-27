using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private Transform interactionTarget;

    private void Start()
    {
        GameManager.OnInteractButtonPressed.AddListener(InteractionHandler);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.GameState != GameStates.Activity && other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Select();
            interactionTarget = other.transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (GameManager.Instance.GameState != GameStates.Activity && other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Deselect();
            interactionTarget = null;
        }
    }

 
    public void InteractionHandler()
    {
        if (interactionTarget != null && interactionTarget.GetComponent<IInteractable>() != null)
        {
            interactionTarget.GetComponent<IInteractable>().Interact();
        }
    }
}
