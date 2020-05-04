using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallInputController : MonoBehaviour
{
    public float cameraOffset = 1f;
    public float rotationSpeed = 1f;
    public Transform directionTarget;

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = cameraOffset;
        directionTarget.position = Camera.main.ScreenToWorldPoint(temp);

        transform.LookAt(directionTarget, Vector3.up);


        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.2f)
        {



            ////Rotate player towards movement
            //Vector3 lookDirection = new Vector3(transform.forward.x + transform.localPosition.x, 0f, 1f);


            //Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            //float step = rotationSpeed * Time.deltaTime;
            //transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);
        }
    }
}
