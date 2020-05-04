
using UnityEngine;
using UnityEngine.UI;

public class ProgressRowController : MonoBehaviour
{

    public bool Completed
    {
        get
        {
            return completed;
        }

        set
        {
            completed = value;
            if(value == true)
            {
                check.sprite = checkedIcon;
            }
            else
            {
                check.sprite = uncheckedIcon;
            }
        }
    }

    private Image check;

    [SerializeField] private Sprite checkedIcon;
    [SerializeField] private Sprite uncheckedIcon;
    [SerializeField] private bool completed;



    public void SetCheck(bool targetCheck)
    {
        Completed = targetCheck;
    }


}
