using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingBroomControl : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private SphereCollider broomCollider;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            broomCollider.enabled = true;
            trail.enabled = true;
        }
        else if(Input.GetMouseButton(0))
        {
            transform.localPosition = new Vector3(inputManager.input.x, transform.localPosition.y, transform.localPosition.z);

        }
        else if(Input.GetMouseButtonUp(0))
        {
            broomCollider.enabled = false;
            trail.enabled = false;
        }

       
    }
}
