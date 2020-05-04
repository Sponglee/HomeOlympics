using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketUITextUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        BasketBallController.OnBasketScoreChanged.AddListener(UpdateText);
    }
}
