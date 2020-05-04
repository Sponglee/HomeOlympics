
using UnityEngine;

public class FencingOpponenController : MonoBehaviour
{
    public HingeJoint joint;
    public float velocity = 500f;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("FencingPlayer"))
        {
            Debug.Log("COLLIDED");
            var hjm = joint.motor;
            hjm.targetVelocity = -hjm.targetVelocity;
           

            joint.motor = hjm;
        }
    }
}
