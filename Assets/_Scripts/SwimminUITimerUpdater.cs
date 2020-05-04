
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
        float secTimer = timer;
        int minutes = Mathf.FloorToInt(secTimer / 60F);
        int seconds = Mathf.FloorToInt(secTimer%60f);
        int miliseconds = Mathf.FloorToInt((secTimer%60f - (float)seconds)*100f);
        string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes,seconds, miliseconds);
        transform.GetComponent<TextMeshProUGUI>().text = niceTime;
    }


}
