using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingCartControl : MonoBehaviour
{
    [SerializeField] private bool EnemyCart = false;
    [SerializeField]
    private int reachedTarget = 0;
    public int ReachedTarget
    {
        get
        {
            return reachedTarget;
        }

        set
        {
            if (EnemyCart)
                reachedTarget = -value;
            else
                reachedTarget = value;
        }
    }

    public void ResultReact()
    {
        transform.GetComponent<QuickOutline>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("CurlingCart"))
        {
            AudioManager.Instance.PlaySound("curling_hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CurlingCarpet"))
        {
            ReachedTarget = 1;
        }
        else if (other.CompareTag("CurlingCarpet1"))
        {
            ReachedTarget = 2;
        }
        else if (other.CompareTag("CurlingCarpet2"))
        {
            ReachedTarget = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CurlingCarpet"))
        {
            ReachedTarget = 0;
        }
        else if (other.CompareTag("CurlingCarpet1"))
        {
            ReachedTarget = 1;
        }
        else if (other.CompareTag("CurlingCarpet2"))
        {
            ReachedTarget = 2;
        }

    }

}
