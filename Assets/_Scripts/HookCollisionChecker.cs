using System.Collections;
using UnityEngine;

public class HookCollisionChecker : MonoBehaviour
{

    public bool HasCollided = false;
    public BoxingController boxingController;

    public void ResetCollidedHand()
    {
       HasCollided = false;
    }

    [SerializeField] private int handIndex;

    private void OnTriggerEnter(Collider other)
    {
        if(!HasCollided)
        {
            //Debug.Log(boxingController.boxingTargetPrefs[handIndex].tag + " : " + other.transform.tag);
            if (other.transform.CompareTag(boxingController.boxingTargetPrefs[handIndex].tag))
            {
                HasCollided = true;
                Invoke(nameof(ResetCollidedHand), 0.2f);
                boxingController.TargetDestroyedHandler(other.gameObject);
            }
            else
            {
                boxingController.MissedHitHandler(transform);
            }
        }
       
    }

}
