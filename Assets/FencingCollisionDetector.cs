using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingCollisionDetector : MonoBehaviour
{
    [SerializeField] private SwingController swingController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("FencingOpponent"))
        {
            Debug.Log("PP");
            swingController.forward = false;
            swingController.Thrust();
        }

    }
}
