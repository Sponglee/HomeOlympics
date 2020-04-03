using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Select();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Deselect();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                other.GetComponent<IInteractable>().Interact();
            }
        }
    }
}
