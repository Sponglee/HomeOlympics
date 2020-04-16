using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingUITextUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        CurlingController.OnCurlingScoreChanged.AddListener(UpdateText);
        UpdateText("0");
    }

}
