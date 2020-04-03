using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelProgressUpdater : MonoBehaviour
{
    private void Start()
    {
        //GameManager.OnProgressChange.AddListener(UpdateProgressSlider);
    }


    private void UpdateProgressSlider(float value)
    {
        transform.GetComponent<Slider>().value = value;
    }
}
