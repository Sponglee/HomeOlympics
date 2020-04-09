using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingUITimerUpdater : UITimeUpdater
{
    public override void SetUpEventListener()
    {
        BoxingController.OnBoxingTimerChanged.AddListener(UpdateText);
    }
}
