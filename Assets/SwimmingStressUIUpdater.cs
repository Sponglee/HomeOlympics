using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimmingStressUIUpdater : MonoBehaviour
{
    public Slider slider;



    private void Start()
    {
        SwimmingPlayerController.onStressChange.AddListener(UpdateSliderUI);
    }


    private void UpdateSliderUI(float stressValue)
    {
        slider.value = stressValue;
    }

}
