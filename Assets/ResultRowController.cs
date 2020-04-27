using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultRowController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;
    [SerializeField] private Image flag;
    [SerializeField] private Image medal;


    public void UpdateRowInfo(string name, string score, Sprite flagSprite, Color medalColor)
    {
        Debug.Log(name + " : " + score);
        playerName.text = name;
        playerScore.text = score;
        flag.sprite = flagSprite;
        medal.color = medalColor;
    }


}
