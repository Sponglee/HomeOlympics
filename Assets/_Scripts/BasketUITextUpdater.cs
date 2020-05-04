

public class BasketUITextUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        BasketBallController.OnBasketScoreChanged.AddListener(UpdateText);
    }
}
