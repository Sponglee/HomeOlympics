using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingTargetBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            BoxingController.TargetDestroyed.Invoke(gameObject);
        }
    }
}
