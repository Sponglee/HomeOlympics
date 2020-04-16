using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class StateChangeBase : MonoBehaviour
{

    public GameStates stateName;
    public CinemachineVirtualCamera stateCam;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.UpdateState.AddListener(ChangeState);
    }
    
    private void ChangeState(GameStates targetState, Transform target = null)
    {
        if (targetState == stateName)
        {
            //Prevent selecting multiple activities through interact
            if (GameManager.Instance.GameState == GameStates.Activity && target != null && target != transform)
                return;

            //Debug.Log("STATECHANGED");
            CameraManager.Instance.SetLive(stateCam);
            StateChangeActionOn();
        }
        else
        {
            if(GameManager.Instance.lastState == stateName)
                StateChangeActionOff();
        }
    }

   

    public abstract void StateChangeActionOn();
    public abstract void StateChangeActionOff();

}
