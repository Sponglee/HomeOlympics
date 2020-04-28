using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public string name;
    public Sprite playerFlag;

    public Color[] medalColors;


    private void Start()
    {
        name = PlayerPrefs.GetString("PlayerName", "Player1");
        playerFlag = PlayerInfoManager.Instance.GrabFlagImage(PlayerPrefs.GetString("PlayerFlag","us"));
    }
}
