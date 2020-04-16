using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingUITextUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        BoxingController.OnBoxingScoreChanged.AddListener(UpdateText);
        UpdateText("0");
    }
}
