using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingRoundUIUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        CurlingController.OnCurlingRoundChanged.AddListener(UpdateText);
    }
}
