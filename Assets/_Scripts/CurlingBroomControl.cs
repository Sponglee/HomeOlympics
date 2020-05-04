
using UnityEngine;

public class CurlingBroomControl : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private SphereCollider broomCollider;
    [SerializeField] private float broomSpeed = 1f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            broomCollider.enabled = true;
            trail.emitting = true;
            MoveBroom(transform.localPosition, new Vector3(0.5f, 0f, transform.localPosition.z));
        }
        else if(Input.GetMouseButton(0))
        {
            transform.localPosition = new Vector3(inputManager.input.x, 0f, transform.localPosition.z);

        }
        else if(Input.GetMouseButtonUp(0))
        {
            broomCollider.enabled = false;
            trail.emitting =false;
            //transform.localPosition = new Vector3(-1f, 0f, 0f);

            MoveBroom(transform.localPosition, new Vector3(-1f, 0f, 0f));
        }

       
    }

    private void MoveBroom(Vector3 startPos, Vector3 endPos)
    {
        transform.localPosition = Vector3.Lerp(startPos,endPos,broomSpeed/Time.fixedDeltaTime);
    }


}
