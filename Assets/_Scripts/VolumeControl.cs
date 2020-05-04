
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{

    public Slider slider;
    public Sprite volumeIcon;
    public Sprite volumeMute;

    public GameObject volumeUI;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("PlayerVolume", 1);
    }

    public void VolumeHandler(float value)
    {
        AudioManager.Instance.VolumeChange(value);

        //volumeUI = GameObject.FindGameObjectWithTag("volume");

        if (value == 0)
        {
            volumeUI.GetComponent<Image>().sprite = volumeMute;
        }
        else
            volumeUI.GetComponent<Image>().sprite = volumeIcon;

    }
}
