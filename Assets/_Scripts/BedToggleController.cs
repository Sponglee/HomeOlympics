
using UnityEngine;

public class BedToggleController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
