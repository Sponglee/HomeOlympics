

public class BasketUITimerUpdater : UITimeUpdater
{
    public override void SetUpEventListener()
    {
        BasketBallController.OnBasketTimerChanged.AddListener(UpdateText);
    }

}
