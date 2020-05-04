using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BasketRoll"))
        {
            BasketBallController.OnBasketDunked.Invoke();
            Destroy(other.gameObject);
        }
    }
}
