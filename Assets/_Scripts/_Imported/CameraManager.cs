using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraManager : Singleton<CameraManager>
{



    [SerializeField] private CinemachineVirtualCamera liveCam = null;

    public GameObject[] cameras;

    void Awake()
    {


        cameras = GrabGameObjectCollection("VCam");

    }


    private void Start()
    {
        //PlayerController.FinishTarget.AddListener(SetFinishTargetCam);
    }

    public void SetFinishTargetCam(Transform target = null)
    {
        liveCam.m_Follow = target;
        liveCam.m_LookAt = target;
    }


    public void SetLive(CinemachineVirtualCamera targetCam)
    {
        if(targetCam != null)
        {
            foreach (var cam in cameras)
            {
                if (cam.transform == targetCam.transform)
                {
                    //Set active cam to live 
                    liveCam = targetCam;
                    liveCam.m_Priority = 10;
                }
                else
                {
                    //If not active gamestate - disable camera and canvas
                    cam.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
                }
            }
        }
       
    }




    public GameObject[] GrabGameObjectCollection(string tag)
    {
        GameObject[] tmpObjects = GameObject.FindGameObjectsWithTag(tag);

        Debug.Log(tmpObjects.Length);

        return tmpObjects;
    }

}