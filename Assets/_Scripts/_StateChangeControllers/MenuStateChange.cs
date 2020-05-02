using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStateChange : StateChangeBase
{
    public override void StateChangeActionOff()
    {
        //Cursor.visible = false;
        FunctionHandler.Instance.ToggleMenuOff();
    }

    public override void StateChangeActionOn()
    {
        Cursor.visible = true;
        FunctionHandler.Instance.ToggleMenuOn();
    }
}
