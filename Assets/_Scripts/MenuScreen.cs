﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {


    [SerializeField]
    private CanvasGroup fadeGroup;

    private float fadeInSpeed = 0.33f;

    public string LoadSceneName = "";

    // Use this for initialization
    private void Start ()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();

        fadeGroup.alpha = 1;
    }

    // Update is called once per frame
    void Update () {
        //FadeIn
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad;


        if (LoadSceneName != "" && fadeGroup.alpha == 0)
        {
            SceneManager.LoadScene(LoadSceneName);
        }
    }
}
