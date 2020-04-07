using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxingController : ActivityControllerBase
{
    public class BoxingTargetDestroyedEvent : UnityEvent<GameObject> { }
    public static BoxingTargetDestroyedEvent TargetDestroyed = new BoxingTargetDestroyedEvent();

    public GameObject boxingTarget;
    public Transform boxingTargetLocations;

    [SerializeField] private GameObject boxingHands;
    [SerializeField] private GameObject activityUI;
    [SerializeField] private Canvas boxingCanvas;
    [SerializeField] private HookController hookController;
    [SerializeField]private List<GameObject> boxingTargetsHolder = new List<GameObject>();



    private void Start()
    {
        TargetDestroyed.AddListener(TargetDestroyedHandler);    
    }


    public override void InitializeActivity()
    {
        Debug.Log(">"+gameObject.name);
        ToggleSystems();
        SpawnTargets();
    }

    public override void DeactivateActivity()
    {
        Debug.Log("<"+gameObject.name);
        ToggleSystems();
    }


    public void ToggleSystems()
    {
        boxingCanvas.gameObject.SetActive(!boxingCanvas.gameObject.activeSelf);
        activityUI.SetActive(!activityUI.activeSelf);
        boxingHands.SetActive(!boxingHands.activeSelf);
        FunctionHandler.Instance.ToggleUI("Boxing");
        //Gamestuff here

    }


    public void SpawnTargets()
    {
        
        for (int i = 0; i < 3; i++)
        {
            GameObject tmpTarget = Instantiate(boxingTarget
                                                , boxingTargetLocations.GetChild(Random.Range(0,boxingTargetLocations.childCount)).position
                                                , Quaternion.LookRotation(Camera.main.transform.position - transform.position,Vector3.up)
                                                , boxingCanvas.transform);
            boxingTargetsHolder.Add(tmpTarget);
        }
    }


    private void TargetDestroyedHandler(GameObject target)
    {
        boxingTargetsHolder.Remove(target);
        Destroy(target);
        if(boxingTargetsHolder.Count<=0)
        {
            Invoke(nameof(SpawnTargets),0.2f);
        }
    }
}
