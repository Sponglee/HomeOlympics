using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{

    public Slider slider;
    public FirstPersonAIO player;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("PlayerSensitivity", 0.75f);
    }

    public void SensitivityHandler(float value)
    {
        player.mouseSensitivity = 15 * value;
        PlayerPrefs.SetFloat("PlayerSensitivity", value);

    }
}
