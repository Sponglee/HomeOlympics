using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStateAction : StateChangeBase
{
    public PlayerCharacterController fpsController;
    public GameObject walkingStateUI;

    public override void StateChangeActionOff()
    {
        //Debug.Log("off");
        fpsController.enabled = false;
        fpsController.GetComponent<Rigidbody>().isKinematic = true;
        //fpsController.GetComponent<CapsuleCollider>().isTrigger = true;
        walkingStateUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void StateChangeActionOn()
    {
        //Debug.Log("on");
        fpsController.enabled = true;
        fpsController.GetComponent<Rigidbody>().isKinematic = false;
        //fpsController.GetComponent<CapsuleCollider>().isTrigger = false;
        walkingStateUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
