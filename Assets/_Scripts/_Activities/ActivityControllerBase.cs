using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityControllerBase : MonoBehaviour
{
    public GameObject activityUI;
    public ActivityOverlayUIUpdater activityOverlay;

    public Sound[] backgroundSound;

    private int currentBackSound = -1;

    public void ActivateActivity()
    {
        InitializeActivity();

        if(backgroundSound.Length>0)
        {
            currentBackSound = Random.Range(0, backgroundSound.Length);

            if(!AudioManager.Instance.transform.Find(backgroundSound[currentBackSound].name + currentBackSound))
            {
                backgroundSound[currentBackSound].CreateSoundObject(backgroundSound[currentBackSound].name + currentBackSound);
            }
            

            backgroundSound[currentBackSound].Play();
        }



        FunctionHandler.Instance.ToggleUI(transform.GetComponent<ActivityStateChange>().activityName);
        if (activityUI != null)
            activityUI.SetActive(true);
        if(activityOverlay != null)
        {
            activityOverlay.UpdateOverlayInfo(PlayerInfoManager.Instance.name, PlayerInfoManager.Instance.playerFlag);
        }
    }

    public void StopBackSound()
    {
        if (currentBackSound >= 0)
        {
            backgroundSound[currentBackSound].Stop();
        }
    }

    public void DeactivateActivity()
    {

        StopBackSound();

        DeInitializeActivity();
        FunctionHandler.Instance.ToggleUI("0");

        if (activityUI != null)
        {
            activityUI.SetActive(false);
        }
    }


    public virtual void DeInitializeActivity() { }
    public virtual void InitializeActivity() { }


    public void ToggleActivityUIForResults(string score)
    {
        activityUI.SetActive(false);

        ActivityResultInfo tmpInfo = new ActivityResultInfo();
        tmpInfo.ActivityName = transform.GetComponent<ActivityStateChange>().activityName;
        tmpInfo.ActivityScore = score;

        if (!GameManager.Instance.resultsCanvas.activeSelf)
        {
            ResultWindowManager.Instance.OpenResultWindow();
            Debug.Log(tmpInfo.ActivityName + " = " + tmpInfo.ActivityScore);
            StartCoroutine(ResultsInvokeDelay(tmpInfo));
        }

       
    }

    private IEnumerator ResultsInvokeDelay(ActivityResultInfo tmpInfo)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        ResultWindowManager.OnResultsOpened.Invoke(tmpInfo);
    }
}
