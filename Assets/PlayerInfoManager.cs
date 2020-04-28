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
            playerFlag = GrabFlagImage(PlayerPrefs.GetString("PlayerFlag", "us"));
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
        dropDown.value = GrabFlagIndex(PlayerPrefs.GetString("PlayerFlag", "us"));
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

    public Sprite GrabFlagImage(string name)
    {
        foreach (var flag in flags)
        {
            if(name == flag.name)
            {
                return flag;
            }
        }
        return null;
    }

    public int GrabFlagIndex(string name)
    {
        for (int i = 0; i < flags.Length; i++)
        {
            if(flags[i].name == name)
            {
                return i;
            }
        }
        return 0;
    }

    public void DropDownSelection(int index)
    {
        PlayerPrefs.SetString("PlayerFlag", flags[index].name);

        Debug.Log(PlayerPrefs.GetString("PlayerFlag"));
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

}
