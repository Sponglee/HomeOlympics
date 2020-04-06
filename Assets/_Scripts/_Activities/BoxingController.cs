using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingController : ActivityControllerBase
{
    [SerializeField] private GameObject boxingCanvas;
    [SerializeField] private GameObject boxingHands;
    

    [SerializeField] private HookController hookController;


    public override void InitializeActivity()
    {
        Debug.Log(">"+gameObject.name);
        ToggleSystems();
    }

    public override void DeactivateActivity()
    {
        Debug.Log("<"+gameObject.name);
        ToggleSystems();
    }


    public void ToggleSystems()
    {
        boxingCanvas.SetActive(!boxingCanvas.activeSelf);
        boxingHands.SetActive(!boxingHands.activeSelf);
        FunctionHandler.Instance.ToggleUI("Boxing");
        //Gamestuff here
    }

    
}
