using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateChange : StateChangeBase
{
    public override void StateChangeActionOff()
    {
        FunctionHandler.Instance.ToggleMenuOff();
    }

    public override void StateChangeActionOn()
    {
        FunctionHandler.Instance.ToggleMenuOn();
    }
}
