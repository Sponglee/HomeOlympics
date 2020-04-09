using UnityEngine;
using TMPro;

public class UITextUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetUpEventListener();
    }

    public virtual void SetUpEventListener() { }

    // Update is called once per frame
    public void UpdateText(string text)
    {
        transform.GetComponent<TextMeshProUGUI>().text = text;
    }
}

