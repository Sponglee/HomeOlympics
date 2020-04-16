using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

    public bool CanExit = false;
    public GameObject doorCanvas;

private void OnTriggerEnter(Collider coll)
{
    if(coll.CompareTag("Player"))
    {
            if (CanExit)
                GetComponent<Animator>().Play("Door_open");
            else
                doorCanvas.SetActive(true);

    }
}


    private void OnTriggerExit(Collider other)
    {
        if(CanExit)
            GetComponent<Animator>().Play("Door_close");
        else
            doorCanvas.SetActive(false);
    }
}
