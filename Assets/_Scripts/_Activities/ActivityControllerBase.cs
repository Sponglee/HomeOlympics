using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityControllerBase : MonoBehaviour
{
    //reference to activity user interface
    public GameObject activityUI;
    public ActivityOverlayUIUpdater activityOverlay;

    //Commentary sounds 
    [SerializeField] protected Sound[] backgroundSound;
    [SerializeField] private int currentBackSound = -1;


    //Activate 
    public void ActivateActivity()
    {
        //Make sure results are closed
        GameManager.Instance.resultsCanvas.gameObject.SetActive(false);

        //Start initialization
        InitializeActivity();

        //If there're commentary sounds - add them to AudioManager and play
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

        //Open base olympic logo ui
        FunctionHandler.Instance.ToggleBaseUI();

        //Activate UI and update player info(flag/name)
        if (activityUI != null)
            activityUI.SetActive(true);
        if(activityOverlay != null)
        {
            if(GameManager.Instance != null)
                activityOverlay.UpdateOverlayInfo(GameManager.Instance.playerName, GameManager.Instance.playerFlag);
        }
    }

    //Deactivate activity on exit
    public void DeactivateActivity()
    {
        StopBackSound();
        
        DeInitializeActivity();

        //Disable olympic logo ui
        FunctionHandler.Instance.ToggleBaseUI();

        //Disable activity UI
        if (activityUI != null)
        {
            activityUI.SetActive(false);
        }
    }

    //Virtual activation/deactivation for specific activities
    public virtual void DeInitializeActivity() { }
    public virtual void InitializeActivity() { }


    //Open results window
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

    //Delay for proper indication of numbers
    protected IEnumerator ResultsInvokeDelay(string score, string activityName, int decimals)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        ResultWindowManager.OnResultsOpened.Invoke(score,activityName, decimals);
        Cursor.visible = true;
    }
    
    //Stop commentary sounds
    public void StopBackSound()
    {
        if (currentBackSound >= 0)
        {
            backgroundSound[currentBackSound].Stop();
        }
    }

}
