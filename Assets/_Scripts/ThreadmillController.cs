using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadmillController : ActivityControllerBase
{

    public float threadmillSpeed = 1f;
    public Transform threadmillContent;


    private void Update()
    {
        threadmillContent.Translate(threadmillSpeed * Time.deltaTime * Vector3.back /2f);
    }
    public override void DeInitializeActivity()
    {
      
    }

    public override void InitializeActivity()
    {
       
    }
}
