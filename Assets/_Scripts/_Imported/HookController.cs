using System.Collections;
using UnityEngine;


public class HookController : MonoBehaviour
{

    public Transform hookHand;
    public Transform freeHand;

    public Transform hookPivot;
    public Transform hookTarget;

    public float offset = 10f;
    public float retractDuration = 0.5f;
    public bool HasCollided = false;

    //[SerializeField] private PlayerController playerController;
    [SerializeField] private InputManager inputManager;

    void Start()
    {
        inputManager = InputManager.Instance;
    }

   
    public void SwapHands()
    {
        //Debug.Log("SWAP");
        //StartCoroutine(MoveHand(hookHand, calculatedHandPosition));

        Transform tmpHand = hookHand;
        hookHand = freeHand;
        freeHand = tmpHand;

        //SwapHands event for model
        
        //StartCoroutine(RetractHand(hookHand, -hookHand.parent.localPosition));
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
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 8;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f,layerMask))
            {
                //Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                //Debug.DrawLine(Camera.main.transform.position, hit.point,Color.red,2f);
                StartCoroutine(MoveHandSequence(hookHand, hit.point));
            }

        }
        else if(Input.GetMouseButton(0))
        {
            //SwapHands(Vector3.zero);
        }
        else if (Input.GetMouseButtonUp(0))
        {

            //SwapHands(Vector3.zero);


        }
    }

    private IEnumerator MoveHandSequence(Transform destHand, Vector3 destination)
    {
        yield return StartCoroutine(MoveHand(destHand, destination));
        StartCoroutine(MoveHand(destHand, destHand.parent.position));
        SwapHands();
    }


    //private void LateUpdate()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
           
    //        hookPivot.localPosition = new Vector3(-offset * Mathf.Clamp(inputManager.input.x, -1f, 1f), -offset * inputManager.input.y, 0f) - Vector3.up * 1.5f;
    //        freeHand.position = hookPivot.position;
    //    }
    //}

}
