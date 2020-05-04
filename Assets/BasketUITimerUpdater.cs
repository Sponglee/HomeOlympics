using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketUITimerUpdater : UITimeUpdater
{
    public override void SetUpEventListener()
    {
        BasketBallController.OnBasketTimerChanged.AddListener(UpdateText);
    }

}
