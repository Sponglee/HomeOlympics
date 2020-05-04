using UnityEngine;

public class ProgressManager : Singleton<ProgressManager>
{
    public Transform[] Activities;
    public int activityFinishedCount = 0;

    public Transform progressCanvas;

    public Doors door;

    public void CanExit()
    {
        door.CanExit = true;
    }
}
