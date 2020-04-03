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
        StateController.UpdateState.AddListener(ChangeState);
    }
    
    private void ChangeState(GameStates targetState)
    {
        if (targetState == stateName)
        {
            CameraManager.Instance.SetLive(stateCam);
            StateChangeActionOn();
        }
        else
        {
            StateChangeActionOff();
        }
    }

    public abstract void StateChangeActionOn();
    public abstract void StateChangeActionOff();

}
