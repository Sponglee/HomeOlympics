using System.Collections;
using UnityEngine;


public class HookController : MonoBehaviour
{
    public bool CanHook = false;
    //private bool collided = false;

    public Transform hookHand;
    public Transform freeHand;

    public Transform hookPivot;
    public Transform hookTarget;

    public float offset = 10f;
    public float retractDuration = 0.5f;
    public bool HasCollided = false;

  
   
    public void SwapHands()
    {
        Transform tmpHand = hookHand;
        hookHand = freeHand;
        freeHand = tmpHand;
    }

    private IEnumerator MoveHand(Transform destHand, Vector3 destination)
    {
        float elapsed = 0f;
        Vector3 startPosition = destHand.position;
        Transform startGizmo = destHand;

        while (elapsed <= retractDuration)
        {
            startGizmo.position = Vector3.Lerp(startPosition, destination, elapsed / retractDuration);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }
        yield return new WaitForFixedUpdate();
        HasCollided = false;
       
    }

    void LateUpdate()
    {
        if(CanHook)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int layerMask = 1 << 8;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
                {
                    StartCoroutine(MoveHandSequence(hookHand, hit.point));
                }
            }
        }
       
    }

    private IEnumerator MoveHandSequence(Transform destHand, Vector3 destination)
    {
        yield return StartCoroutine(MoveHand(destHand, destination));
        StartCoroutine(MoveHand(destHand, destHand.parent.position));
        SwapHands();
    }


    public void RetractAll()
    {
        StartCoroutine(MoveHand(hookHand, hookHand.parent.position));
        StartCoroutine(MoveHand(freeHand, freeHand.parent.position));
    }
}


