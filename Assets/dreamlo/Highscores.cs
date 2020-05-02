using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;



public class Highscores : Singleton<Highscores>
{
    
    public Text errorText;
    public string[] privateCodes;
    public string[] publicCodes;


    public string privateCode;
    public string publicCode;
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    public void Start()
    { 
        
       
    }

    public void AddNewHighscore(string score, int dbIndex)
    {
        privateCode = privateCodes[dbIndex];
        StartCoroutine(UploadNewHighscore(PlayerPrefs.GetString("PlayerName","Player1"), score, PlayerPrefs.GetInt("PlayerFlag",0)));
    }

    IEnumerator UploadNewHighscore(string username, string score, int flagId)
    {

        username = Clean(username);
      
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score + "/" + flagId + "/" + 0);

        Debug.Log(www.url);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error uploading: " + www.error);
        }

    }

    public void DownloadHighscores(int dbIndex, int decimals = 0)
    {
     
            //leaderButtons[i].color = new Color32(171, 61, 50, 255);
            //leaderButtons[i].color = Color.white;

        publicCode = publicCodes[dbIndex];
        StartCoroutine(DownloadHighscoresFromDatabase(decimals));
    }

    private IEnumerator DownloadHighscoresFromDatabase(int decimals)
    {
        yield return new WaitForSecondsRealtime(0.1f);  
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text,decimals);
        }
        else
        {
           //errorText = GameObject.FindGameObjectWithTag("DebugName").GetComponent<Text>();
           errorText.text = www.error + "\nPlease try again later..";
        }
    }

    //Clean off unwanted characters
    string Clean(string s)
    {
        s = s.Replace("/", "");
        s = s.Replace("|", "");
        return s;

    }

    //Prefabs for Score Display

    public GameObject scoreElementPref;
    public Transform scoreElementContainer;
   
    //Format and display highscores
    void FormatHighscores(string textStream,int decimals)
    {
        //grab a reference for a container
        //scoreElementContainer = GameObject.FindGameObjectWithTag("LeaderContainer").transform;


        //errorText = GameObject.FindGameObjectWithTag("DebugName").GetComponent<Text>();
        errorText.enabled = false;

        Debug.Log(">>>>>>>>>>"+textStream);
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];


        //Reset scoreboard
        foreach (Transform child in scoreElementContainer)
        {
            Destroy(child.gameObject);
        }

        //Add every entry to the list
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            float score = (float)Math.Round(float.Parse(entryInfo[1]), decimals);
            int flagId = int.Parse(entryInfo[2]);
            highscoresList[i] = new Highscore(username, score.ToString(), flagId);





            GameObject tmp = Instantiate(scoreElementPref, scoreElementContainer);

            tmp.GetComponent<ResultRowController>().UpdateRowInfo(username, score.ToString(), PlayerInfoManager.Instance.flags[flagId], Color.white, decimals);


            if (highscoresList[i].username == PlayerPrefs.GetString("PlayerName","Player1"))
            {
                tmp.transform.GetComponentInChildren<Image>().color = new Color32 (255,247,100,255);
              
            }
        }
    }

}

public struct Highscore
{
    public string username;
    public string score;
    public int flag;

    public Highscore(string _username, string _score, int _flag)
    {
        username = _username;
        score = _score;
        flag = _flag;
    }

}