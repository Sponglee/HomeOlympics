using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;

public class ResultRowController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private Image flag;
    [SerializeField] private Image medal;


    public void UpdateRowInfo(string name, string score, Sprite flagSprite, Color medalColor, int decimals = 0)
    {
        Debug.Log(name + " : " + score);
        StartCoroutine(StartUpdateDelay(name, score, flagSprite, medalColor,decimals));
    }

    private IEnumerator StartUpdateDelay(string name, string score, Sprite flagSprite, Color medalColor,int decimals)
    {
        Debug.Log("DECIMALS " + decimals + " : " + ((float)Math.Round(Mathf.Abs(float.Parse(score))/Math.Pow(10f,decimals), decimals)).ToString());
        yield return new WaitForSecondsRealtime(0.1f);
        playerName.text = name;
        playerScore.text = ((float)Math.Round(Mathf.Abs(float.Parse(score)) / Math.Pow(10f, decimals), decimals)).ToString();
        flag.sprite = flagSprite;
        medal.color = medalColor;
    }
}
