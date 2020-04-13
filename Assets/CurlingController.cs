using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingController : ActivityControllerBase
{
    [SerializeField] private Transform curlingHide;
    [SerializeField] private Transform curlingContent;
    

    [SerializeField] private Transform cart;
    [SerializeField] private Transform directionTarget;
    [SerializeField] private Transform arrow;

    public override void InitializeActivity()
    {
        curlingContent.gameObject.SetActive(true);
        curlingHide.gameObject.SetActive(true);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cart.GetComponent<Rigidbody>().AddForce(new Vector3(arrow.forward.x, cart.GetComponent<Rigidbody>().velocity.y, arrow.forward.z) * 200f);
        }
    }

    public override void DeInitializeActivity()
    {
        base.DeInitializeActivity();
    }

    private void ToggleSystems()
    {
       
    }
}
