//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class PlayerController : MonoBehaviour
//{
//    //Transform of a player to get level progression
//    public Transform playerTransform;

//    //public Transform hookedTarget;
//    public bool CanJump = true;
//    public Rigidbody rb;


//    [SerializeField] private InputManager inputManager;
//    [SerializeField] private HookController hookController;
//    [SerializeField] private float playerSpeed = 130f;


//    [SerializeField] private float calculatedInputX;
//    public float CalculatedInputX
//    {
//        get
//        {
//            return calculatedInputX;
//        }

//        set
//        {
//            //if(Mathf.Sign(value) != Mathf.Sign(calculatedInputX) && value != 0f)
//            //{
//            //    //hookController.SwapHands(hookController.freeHand.InverseTransformPoint(hookController.freeHand.position));
//            //}
//            calculatedInputX = value;
//        }
//    }

//    private void Start()
//    {
//        playerTransform = transform;
//        rb = GetComponent<Rigidbody>();
//        rb.velocity = Vector3.zero;
//        inputManager = InputManager.Instance;
//    }


//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            StopJump();
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            Jump(hookController.freeHand);

//            hookController.SwapHands(Vector3.zero);

//        }
//    }

//    private void LateUpdate()
//    {
//        if (Input.GetMouseButton(0))
//        {
//            //Calculate input to swap hands in runtime
//            //rb.velocity = (hookController.freeHand.position + Vector3.up * 1.5f - transform.position).normalized * playerSpeed/3f;

//            hookController.hookPivot.localPosition = new Vector3(-hookController.offset * Mathf.Clamp(inputManager.input.x, -1f, 1f), -hookController.offset * inputManager.input.y, 0f) - Vector3.up * 1.5f;
//            hookController.freeHand.position = hookController.hookPivot.position;
//        }
//    }

//    public void StopJump()
//    {
//        if (CanJump)
//        {
//            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
//        }
//    }


//    public void Jump(Transform targetHand)
//    {
//        if (CanJump && Vector3.Distance(targetHand.position, transform.position) >= 0.1f)
//        {
//            rb.useGravity = true;
//            rb.velocity = (targetHand.position + Vector3.up * 1.5f - transform.position).normalized * playerSpeed;
//            CanJump = false;
//        }
//    }


//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Rock"))
//        {
//            MoveToTarget(other.transform);
//        }
//    }



//    public void MoveToTarget(Transform target)
//    {
//        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z) /*- new Vector3(hookController.freeGizmo.parent.position.x, 0f, 0f)*/;
//        rb.velocity = Vector3.zero;
//        rb.useGravity = false;
//        CanJump = true;
//        StartCoroutine(MoveTransformToPosition(target.position));
//    }

//    private IEnumerator MoveTransformToPosition(Vector3 targetPosition)
//    {
//        float elapsed = 0f;
//        float duration = 0.3f;
//        Vector3 startPos = transform.position;
//        Vector3 destPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
//        while (elapsed <= duration)
//        {
//            //Debug.Log("!");
//            transform.position = Vector3.Lerp(startPos, destPosition, elapsed / duration);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Boundary"))
//        {
//            GameManager.OnLevelComplete.Invoke("GAME OVER");
//        }
//    }



//}
