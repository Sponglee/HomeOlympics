using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingStateAction : StateChangeBase
{
    public FirstPersonAIO fpsController;

    public override void StateChangeActionOff()
    {
        fpsController.enabled = false;
    }

    public override void StateChangeActionOn()
    {
        fpsController.enabled = true;
    }
}
