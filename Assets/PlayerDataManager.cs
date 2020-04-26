using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : Singleton<PlayerDataManager>
{
    public string name;
    public Sprite playerFlag;

    public Sprite[] flags;
    public Color[] medalColors;

}
