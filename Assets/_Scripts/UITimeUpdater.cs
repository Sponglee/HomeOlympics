
using TMPro;
using UnityEngine;

public class UITimeUpdater : MonoBehaviour
{
    private void Start()
    {
        SetUpEventListener();
    }

    public virtual void SetUpEventListener(){ }

    public void UpdateText(int timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        transform.GetComponent<TextMeshProUGUI>().text = niceTime;
    }
   
}
