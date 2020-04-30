using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FencingController : ActivityControllerBase
{

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
