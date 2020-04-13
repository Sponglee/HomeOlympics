using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public InputManager inputManager;
    
    public float rotationSpeed = 1f;
    public Vector2 inputRange = new Vector2(1,1);
    public Vector2 offset = Vector2.zero;

    private void Start()
    {
        //inputManager = InputManager.Instance;
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            MoveTransform();
        }
    }
    private void MoveTransform()
    {
        //Move a player with rb velocity forward + joystick offsets
    

        //Offset player within a screen
        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);

        

        //Rotate player towards movement
        Vector3 lookDirection =  new Vector3(0f/*transform.forward.x +(-inputManager.input.y)*inputRange.y*/, transform.forward.x + (-inputManager.input.x*inputRange.x) *5f, 1f);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, step);

    }
}
