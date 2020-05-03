using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwimmingCanvasOverlayController : MonoBehaviour

{

    public Image flagGraphic;
    public TextMeshProUGUI numberText;

    public void ShowGraphic(int index, Sprite flag)
    {
        flagGraphic.sprite = flag;
        numberText.text = (index+1).ToString();
    }


}
