using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    public Joystick joystick;

    public Vector2 input;

    private void OnEnable()
    {
        joystick.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        joystick.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            input = new Vector2(joystick.Horizontal, joystick.Vertical);

        }
    }
}
