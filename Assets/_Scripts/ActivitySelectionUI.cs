using UnityEngine;
using TMPro;

public class ActivitySelectionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;


    public void UpdateSelectionText(string name)
    {
        text.text = name;
    }


}
