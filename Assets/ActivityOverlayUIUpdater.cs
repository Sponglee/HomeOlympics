using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityOverlayUIUpdater : MonoBehaviour
{

    [SerializeField] private Image flagHolder;
    [SerializeField] private TextMeshProUGUI playerName;


    public void UpdateOverlayInfo(string name, Sprite flag)
    {
        flagHolder.sprite = flag;
        playerName.text = name;
    }

}
