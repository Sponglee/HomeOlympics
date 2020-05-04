
using UnityEngine;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{

    public Slider slider;
    public PlayerCharacterController player;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("PlayerSensitivity", 0.75f);
        player.rotationSpeed = 400f * slider.value;
    }

    public void SensitivityHandler(float value)
    {
        player.rotationSpeed = 400f * value;
        PlayerPrefs.SetFloat("PlayerSensitivity", value);

    }
}
