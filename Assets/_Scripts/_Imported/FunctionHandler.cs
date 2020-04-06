using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FunctionHandler : Singleton<FunctionHandler>
{

    public Transform menuCanvas;
    public Transform winCanvas;
    public Transform uiCanvas;

    public Text menuText;

    public CinemachineVirtualCamera menuCam;


    public void TogglePauseState()
    {
        if (menuCam.gameObject.activeSelf)
        {
            //Check if there was a previous state to get back to it (GameStart)?
            if(GameManager.Instance.lastState != GameManager.Instance.GameState)
            {
                GameManager.Instance.GameState = GameManager.Instance.lastState;
            }
            else
                GameManager.Instance.GameState = GameStates.Walking;
        }
        else
        {

            GameManager.Instance.GameState = GameStates.Paused;
        }
    }

    public void ToggleUI(string activityName)
    {
        uiCanvas.gameObject.SetActive(!uiCanvas.gameObject.activeSelf);
    }

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }


    public void LevelComplete()
    {
        menuText.text = "YOU WIN";
        uiCanvas.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(true);
        Time.timeScale =0f;
    }

    public void ToggleMenuOn()
    {
        menuCanvas.gameObject.SetActive(true);

        //if(Time.timeScale == 1f)
        //    Time.timeScale = 0f;
        //else if(Time.timeScale == 0f)
        //    Time.timeScale = 1f;
    }
    public void ToggleMenuOff()
    {
        menuCanvas.gameObject.SetActive(false);
    }

    public void GameOver() 
    {
        //menuText.text = GameManager.Instance.Score.ToString();
        uiCanvas.gameObject.SetActive(!uiCanvas.gameObject.activeSelf);
        winCanvas.gameObject.SetActive(!winCanvas.gameObject.activeSelf);

        if (Time.timeScale == 1f)
            Time.timeScale = 0f;
        else if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }


}
