using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwimmingPlayerController : SwimmingSwimmer
{
    public class UpdateStressEvent : UnityEvent<float> { }
    public static UpdateStressEvent onStressChange  = new UpdateStressEvent();

    public class UpdateTimerEvent : UnityEvent<float> { }
    public static UpdateTimerEvent OnTimerChange = new UpdateTimerEvent();

    public override float StressLevel
    {
        get
        {
            return base.StressLevel;
        }

        set
        {
            base.StressLevel = value;
            onStressChange.Invoke(stressLevel/stressLimit);
        }
    }

    public override float SwimmerResultTime
    {
        get
        {
            return base.SwimmerResultTime;
        }

        set
        {
            base.SwimmerResultTime = (float)Math.Round(value,2);
            
            OnTimerChange.Invoke(value);
        }
    }

    private void Start()
    {
        if (PlayerInfoManager.Instance != null)
            flag = PlayerInfoManager.Instance.playerFlag;
        else
            flag = GameManager.Instance.playerFlag;

        onStressChange.Invoke(stressLevel / stressLimit);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Push();
        }
    }

    protected override void Finished(float time, LaneController controller)
    {
        SwimmingController.onFinish.Invoke(time, controller, SwimmerResultTime);
        base.Finished(time, controller);
    }
}
