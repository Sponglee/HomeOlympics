using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingCartControl : MonoBehaviour
{
    

    [SerializeField] private int finalScore = 0;
    public int FinalScore
    {
        get
        {
            return finalScore;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CurlingCarpet"))
        {
            finalScore = 1;
        }
        else if(other.CompareTag("CurlingCarpet1"))
        {
            finalScore = 2;
        }
        else if(other.CompareTag("CurlingCarpet2"))
        {
            finalScore = 3;
        }
    }

}
