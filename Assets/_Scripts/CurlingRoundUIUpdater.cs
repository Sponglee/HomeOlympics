

public class CurlingRoundUIUpdater : UITextUpdater
{
    public override void SetUpEventListener()
    {
        CurlingController.OnCurlingRoundChanged.AddListener(UpdateText);
    }
}
