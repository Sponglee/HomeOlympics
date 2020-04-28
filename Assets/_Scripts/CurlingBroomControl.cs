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
            trail.emitting = true;
        }
        else if(Input.GetMouseButton(0))
        {
            transform.localPosition = new Vector3(inputManager.input.x, 0f, transform.localPosition.z);
          

        }
        else if(Input.GetMouseButtonUp(0))
        {
            broomCollider.enabled = false;
            trail.emitting =false;
            transform.localPosition = new Vector3(-1f, 0f, 0f);
        }

       
    }
}
