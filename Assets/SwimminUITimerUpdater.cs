using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwimminUITimerUpdater : UITimeUpdater
{
    private void Start()
    {
        SwimmingPlayerController.OnTimerChange.AddListener(UpdateSwimmingText);
    }


    public void UpdateSwimmingText(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer);
        int miliseconds = Mathf.FloorToInt((timer - (float)seconds)*100f);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes,seconds, miliseconds);
        transform.GetComponent<TextMeshProUGUI>().text = niceTime;
    }


}
