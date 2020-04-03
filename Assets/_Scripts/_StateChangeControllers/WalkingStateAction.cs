using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStateAction : StateChangeBase
{
    public FirstPersonAIO fpsController;

    public override void StateChangeActionOff()
    {
        fpsController.enabled = false;
        fpsController.transform.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void StateChangeActionOn()
    {
        fpsController.enabled = true;
        fpsController.transform.GetComponent<Rigidbody>().isKinematic = false;
    }
}
