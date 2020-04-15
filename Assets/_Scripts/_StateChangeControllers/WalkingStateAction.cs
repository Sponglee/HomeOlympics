using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStateAction : StateChangeBase
{
    public FirstPersonAIO fpsController;

    public override void StateChangeActionOff()
    {
        Debug.Log("off");
        fpsController.enabled = false;
        fpsController.GetComponent<Rigidbody>().isKinematic = true;
        fpsController.GetComponent<CapsuleCollider>().isTrigger = true;
    }

    public override void StateChangeActionOn()
    {
        Debug.Log("on");
        fpsController.enabled = true;
        fpsController.GetComponent<Rigidbody>().isKinematic = false;
        fpsController.GetComponent<CapsuleCollider>().isTrigger = false;
    }
}
