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
        fpsController.gameObject.SetActive(false);
    }

    public override void StateChangeActionOn()
    {
        Debug.Log("on");
        fpsController.enabled = true;
        fpsController.gameObject.SetActive(true);
    }
}
