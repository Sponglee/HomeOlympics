using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBallController : ActivityControllerBase
{

    [SerializeField] private GameObject basketHide;
    [SerializeField] private GameObject basketContent;
    
    [SerializeField] private CinemachineVirtualCamera vcamGame;
    [SerializeField] private CinemachineVirtualCamera vCamState;

    [SerializeField] private GameObject toiletPaperPref;

    public GameObject countDownGraphic;

    public override void DeInitializeActivity()
    {
        basketContent.SetActive(false);
        basketHide.SetActive(true);
        base.DeInitializeActivity();
    }

    public override void InitializeActivity()
    {

        basketContent.SetActive(true);
        basketHide.SetActive(false);
        base.InitializeActivity();
        InitializeGamePlay();
    }

    private void InitializeGamePlay()
    {
        //Debug.Log(">>><<<><>><><");
        //Gamestuff here
        SetUpGamePlay();

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartSwimmingGame());
    }

    public void SetUpGamePlay()
    {
       
    }


    private IEnumerator StartSwimmingGame()
    {
        countDownGraphic.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        countDownGraphic.SetActive(false);
        AudioManager.Instance.PlaySound("boxing_ding");


        StartGame();


    }



    private void StartGame()
    {
        CameraManager.Instance.SetLive(vcamGame);
        //OnSwimmingGameStarted.Invoke();
    }

}
