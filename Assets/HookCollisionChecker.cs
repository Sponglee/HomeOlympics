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


    private void OnTriggerEnter(Collider other)
    {
        if(!HasCollided)
        {
            if (other.CompareTag("BoxingTarget"))
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
