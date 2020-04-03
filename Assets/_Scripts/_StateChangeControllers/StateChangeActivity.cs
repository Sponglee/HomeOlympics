using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeActivity : StateChangeBase
{
    public Canvas activityCanvas;

    public override void StateChangeActionOff()
    {
        FunctionHandler.Instance.uiCanvas.gameObject.SetActive(false);
    }

    public override void StateChangeActionOn()
    {
        
        FunctionHandler.Instance.uiCanvas.gameObject.SetActive(true);
    }
}
