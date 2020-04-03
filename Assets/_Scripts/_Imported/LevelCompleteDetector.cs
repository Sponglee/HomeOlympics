using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //GameManager.OnLevelComplete.Invoke();
        }
    }
}
