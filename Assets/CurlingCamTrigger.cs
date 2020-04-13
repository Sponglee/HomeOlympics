using UnityEngine;
using Cinemachine;

public class CurlingCamTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera targetCam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CurlingCart"))
            CameraManager.Instance.SetLive(targetCam);
    }
}
