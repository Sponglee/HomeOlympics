using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInfoManager : Singleton<PlayerInfoManager>
{
    
    public Sprite[] flags;

    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private InputField inputField;

    public string playerName;
    public Sprite playerFlag;

    public Color[] medalColors;

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            playerName = PlayerPrefs.GetString("PlayerName", "Player1");
            playerFlag = flags[PlayerPrefs.GetInt("PlayerFlag", 0)];
        }
    }
    

    private void Start()
        
    {
        DontDestroyOnLoad(gameObject);

        PopulateDropDown();
        LoadPrefData();
    }

    private void LoadPrefData()
    {
        dropDown.value = PlayerPrefs.GetInt("PlayerFlag", 0);
        inputField.text = PlayerPrefs.GetString("PlayerName", "Player1");
    }


    private void PopulateDropDown()
    {
        dropDown.ClearOptions();

        List<TMP_Dropdown.OptionData> flagItems = new List<TMP_Dropdown.OptionData>();

        foreach (var flag in flags)
        {
            var flagOption = new TMP_Dropdown.OptionData(flag.name, flag);
            flagItems.Add(flagOption);
        }

        dropDown.AddOptions(flagItems);

    }

   

    public void DropDownSelection(int index)
    {
        PlayerPrefs.SetInt("PlayerFlag", index);

        Debug.Log(PlayerPrefs.GetInt("PlayerFlag"));
    }


    public void InputFieldSelection(string input)
    {
        PlayerPrefs.SetString("PlayerName",input);

        Debug.Log(PlayerPrefs.GetString("PlayerName"));
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void TitleQuit()
    {
        Application.Quit();
    }
}
