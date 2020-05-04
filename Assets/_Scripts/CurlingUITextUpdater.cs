

public class CurlingUITextUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        CurlingController.OnCurlingScoreChanged.AddListener(UpdateText);
        UpdateText("0");
    }

}
