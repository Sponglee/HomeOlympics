using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HookController : MonoBehaviour
{
    
    public Transform hookGizmo;
    public Transform hookPivot;

    public float offset = 10f;
    public float retractDuration = 1f;
    public bool HasCollided = false;

    [SerializeField] private InputManager inputManager;
    // Start is called before the first frame update

    

    void Start()
    {

        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
          
        }
        else if (Input.GetMouseButton(0))
        {
            hookPivot.localPosition = new Vector3(0f, -offset * inputManager.input.y, offset * Mathf.Clamp(inputManager.input.x, -100f, 100f));

            RaycastHit hitInfo;
                Debug.DrawRay(transform.position, (hookPivot.position - transform.position).normalized * Vector3.Distance(transform.position, hookPivot.position), Color.red);
            if (Physics.Raycast(transform.position,  (hookPivot.position-transform.position).normalized, out hitInfo, Vector3.Distance(transform.position, hookPivot.position)))
            {
                Debug.Log(hitInfo.transform.name);
                if (!hitInfo.transform.CompareTag("Hook") && !hitInfo.transform.CompareTag("Player"))
                {
                    hookGizmo.position = hitInfo.point;
                    HasCollided = true;
                }
                
            }
            else
                hookGizmo.position = hookPivot.position;

        }
        else if(Input.GetMouseButtonUp(0))
        {

            StartCoroutine(RetractHook());
        }
    }

    private IEnumerator RetractHook()
    {
        float elapsed = 0f;
        Vector3 startPosition = hookGizmo.position;

        while (elapsed<=retractDuration)
        {
           
            hookGizmo.position = Vector3.Lerp(startPosition, transform.position, elapsed/retractDuration);
            elapsed += Time.deltaTime;
            yield return null;

        }


       
       
        HasCollided = false;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.transform.CompareTag("Player"))
    //    {
    //        Debug.Log("HERE");
    //        HasCollided = true;
    //        hookPoint.position = other.ClosestPointOnBounds(transform.position);
    //    }

    //}
}
