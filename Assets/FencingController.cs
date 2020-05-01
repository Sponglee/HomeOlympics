using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FencingController : ActivityControllerBase
{
    public class FencingScoreUpdateEvent : UnityEvent<string> { }
    public static FencingScoreUpdateEvent OnFencingScoreChanged = new FencingScoreUpdateEvent();

    //public class BoxingTimerUpdateEvent : UnityEvent<int> { }
    //public static BoxingTimerUpdateEvent OnBoxingTimerChanged = new BoxingTimerUpdateEvent();

    public GameObject fencingContent;
    public GameObject fencingHide;

    

    public override void DeInitializeActivity()
    {
        base.DeInitializeActivity();
        fencingContent.SetActive(false);
        fencingHide.SetActive(true);
    }

    public override void InitializeActivity()
    {
        base.InitializeActivity();
        fencingContent.SetActive(true);
        fencingHide.SetActive(false);
    }





}
