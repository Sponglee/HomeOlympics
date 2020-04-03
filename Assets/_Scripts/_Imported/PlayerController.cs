//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerController : MonoBehaviour, IInteractable
//{
//    //Transform of a player to get level progression
//    public static Transform playerTransform;
//    private InputManager inputManager;
  
//    public Rigidbody rb;

//    [SerializeField] private HookController hookController;
//    [SerializeField] private float horizontalSpeed=130f;
//    [SerializeField] private float verticalSpeed = 13f;

//    private void OnLevelWasLoaded(int level)
//    {
//        playerTransform = transform;
//    }

//    private void Start()
//    {
//        playerTransform = transform;
//        rb = GetComponent<Rigidbody>();
//        inputManager = InputManager.Instance;
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//        if(Input.GetMouseButtonDown(0))
//        {
//            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);
//            //new Vector3(transform.forward.x + (inputManager.input.x) * inputRange.x, transform.forward.y + (inputManager.input.y * inputRange.y) / 2f, 1f);
//        }
//        else if(Input.GetMouseButtonUp(0))
//        {
//            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + -verticalSpeed * inputManager.input.y, -horizontalSpeed * Mathf.Clamp(inputManager.input.x, -100f, 0f));
//            rb.velocity = Vector3.up * verticalSpeed;

//            if(hookController.HasCollided)
//                rb.velocity += (hookController.hookGizmo.position - transform.position).normalized*horizontalSpeed;
//        }
//    }

  

    


//    public void CollisionInteract(Transform collisionTransform)
//    {
//        Debug.Log("HIT");
//        //FunctionHandler.Instance.GameOver();
//    }

//    public void TriggerInteract(Transform triggerTransform)
//    {
       
//    }
//}
