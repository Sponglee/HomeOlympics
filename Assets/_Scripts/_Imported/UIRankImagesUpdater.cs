using UnityEngine;
using TMPro;

public class UIRankImagesUpdater : MonoBehaviour
{
    void Start()
    {
        SetUpEventListener();
    }

    public virtual void SetUpEventListener() { }
        
    public virtual void UpdateImage(int rank){}


}

