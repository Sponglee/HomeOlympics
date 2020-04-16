﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FltText : MonoBehaviour {
    
    public float timer;
    public float scoreNumber;
    public TextMeshProUGUI scoreNumberText;

    public bool destroyBool = true;

    //Float offset x
    private float xDirection;

    public void Start()
    {
        
        //transform.SetParent(LevelManager.Instance.EffectHolder);
        transform.LookAt(Camera.main.transform);

        timer = Random.Range(0.5f, 1.8f);
        xDirection = Random.Range(0.0f*timer, 0.0f*timer);


        transform.localPosition += new Vector3 (xDirection, 0, 0);
    }
    // Update is called once per frame
    void FixedUpdate () {
        
		if (timer > 0)
        {
           
            timer -= Time.deltaTime;
            transform.localPosition -= new Vector3 (-xDirection, -0.005f, 0);
            transform.localScale += new Vector3(0.005f, 0.005f, 0);
        }
        else
        {
            if(destroyBool)
                Destroy(gameObject);
        }
	}

    void CloseLvlUpWindow()
    {
        Destroy(gameObject);
    }
}
