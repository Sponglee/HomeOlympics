using System.Collections;
using UnityEngine;


public class HookController : MonoBehaviour
{

    public bool HookOnCoolDown
    {
        get
        {
            return hookOnCoolDown;
        }

        set
        {
            if(!hookOnCoolDown && value == true)
            {
                //////
                Invoke(nameof(ResetCoolDown), 0.02f);
            }
            hookOnCoolDown = value;
        }
    }

    private void ResetCoolDown()
    {
        HookOnCoolDown = false;
    }

    public bool CanHook = false;
    //private bool collided = false;

    public Transform hookHand;
    public Transform offHand;

    public Transform hookPivot;
    public Transform hookTarget;

    public float offset = 10f;
    public float retractDuration = 0.5f;
    public bool HasCollided = false;

  
   [SerializeField] private bool hookOnCoolDown = false;

    public void SwapHands()
    {
        Transform tmpHand = hookHand;
        hookHand = offHand;
        offHand = tmpHand;
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
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForFixedUpdate();
        HasCollided = false;
       
    }

    void LateUpdate()
    {
        if(CanHook && !HookOnCoolDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int layerMask = 1 << 8;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
                {
                    StopCoroutine(MoveHandSequence(hookHand, hookHand.parent.position));
                    StartCoroutine(MoveHandSequence(hookHand, hit.point));
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                int layerMask = 1 << 8;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
                {
                    StopCoroutine(MoveHandSequence(offHand, offHand.parent.position));
                    StartCoroutine(MoveHandSequence(offHand, hit.point));
                }
            }
            //else if(Input.GetMouseButtonUp(0))
            //{

            //}
            //else if(Input.GetMouseButtonUp(1))
            //{

            //}
        }
       
    }

    private IEnumerator MoveHandSequence(Transform destHand, Vector3 destination)
    {
        HookOnCoolDown = true;
        yield return StartCoroutine(MoveHand(destHand, destination));
        StartCoroutine(MoveHand(destHand, destHand.parent.position));
        //SwapHands();
    }


    public void RetractAll()
    {
        StartCoroutine(MoveHand(hookHand, hookHand.parent.position));
        StartCoroutine(MoveHand(offHand, offHand.parent.position));
    }
}


