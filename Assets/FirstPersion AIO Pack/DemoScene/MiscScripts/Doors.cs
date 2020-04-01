using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {

private void OnTriggerEnter(Collider coll)
{
    if(coll.CompareTag("Player"))
    {
	     GetComponent<Animator>().Play("Door_open");
	     //this.enabled=false;
    }
}


    private void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().Play("Door_close");
        //this.enabled = true;
    }
}
