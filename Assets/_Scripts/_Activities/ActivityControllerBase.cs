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
                AudioManager.Instance.sounds.Add(backgroundSound[currentBackSound]);
            }
            

            backgroundSound[currentBackSound].Play();
        }



        FunctionHandler.Instance.ToggleBaseUI();
        if (activityUI != null)
            activityUI.SetActive(true);
        if(activityOverlay != null)
        {
            if(PlayerInfoManager.Instance != null)
                activityOverlay.UpdateOverlayInfo(PlayerInfoManager.Instance.playerName, PlayerInfoManager.Instance.playerFlag);

            #if UNITY_EDITOR
                activityOverlay.UpdateOverlayInfo(GameManager.Instance.playerName, GameManager.Instance.playerFlag);
            #endif

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
        FunctionHandler.Instance.ToggleBaseUI();

        if (activityUI != null)
        {
            activityUI.SetActive(false);
        }
    }


    public virtual void DeInitializeActivity() { }
    public virtual void InitializeActivity() { }


    public virtual void ToggleActivityUIForResults(string score, int decimals = 0)
    {
        activityUI.SetActive(false);

       
        string activity = transform.GetComponent<ActivityStateChange>().activityName;
        string scores = score.ToString();

        if (!GameManager.Instance.resultsCanvas.activeSelf)
        {
            ResultWindowManager.Instance.OpenResultWindow();
            Debug.Log(activity + " = " + score);
            StartCoroutine(ResultsInvokeDelay(scores, activity, decimals));
        }

       
    }

    protected IEnumerator ResultsInvokeDelay(string score, string activityName, int decimals)
    {
     
        yield return new WaitForSecondsRealtime(0.1f);
        ResultWindowManager.OnResultsOpened.Invoke(score,activityName, decimals);
        Cursor.visible = true;
    }
}
