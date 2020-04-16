using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingLivesUpdater : UIRankImagesUpdater
{
    public GameObject[] liveImages;

    public override void SetUpEventListener()
    {
        CurlingController.OnCurlingLivesChanged.AddListener(UpdateImage);
    }

    public override void UpdateImage(int lives)
    {
        int index = 0;
        foreach (GameObject live in liveImages)
        {
           
            if(index<lives)
            {
                live.SetActive(true);
                index++;
            }
            else
            {
                live.SetActive(false);
            }
        }
    }

   
}
