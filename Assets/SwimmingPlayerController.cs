using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwimmingPlayerController : SwimmingSwimmer
{
    public class UpdateStressEvent : UnityEvent<float> { }
    public static UpdateStressEvent onStressChange  = new UpdateStressEvent();


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

    private void Start()
    {
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

    


}
