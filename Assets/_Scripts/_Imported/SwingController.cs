using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public InputManager inputManager;

    public float rotationSpeed = 1f;
    public Vector2 inputRange = new Vector2(1, 1);
    public Vector2 offset = Vector2.zero;
    public float cameraOffset = 1.5f;


    public Transform hand;
    public bool forward = false;
    public float thrustOffset = 1f;

    public Vector3 thrustVector = Vector3.zero;


    private void Start()
    {
        //inputManager = InputManager.Instance;
    }
    private void Update()
    {
        MoveTransform();

        if(Input.GetMouseButtonDown(0))
        {
            forward = true;
            Thrust();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            forward = false;
            Thrust();
        }
    }
    private void MoveTransform()
    {
        //Move a player with rb velocity forward + joystick offsets


        //Offset player within a screen
        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);


        Vector3 temp = Input.mousePosition;
        temp.z = cameraOffset;
        transform.position = Camera.main.ScreenToWorldPoint(temp) + thrustVector;


        //Rotate player towards movement
        Vector3 lookDirection = new Vector3(transform.forward.x + (transform.localPosition.x) * inputRange.x,0f, 1f);
     

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);

    }


    public void Thrust()
    {
        if (forward)
            thrustVector = Vector3.left * thrustOffset;
        else
            thrustVector = Vector3.zero;
    }

}
