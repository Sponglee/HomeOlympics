using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingSwimmer : MonoBehaviour
{
    public virtual float StressLevel
    {
        get
        {
            return stressLevel;
        }

        set
        {
            stressLevel = value;
            
            if (value > stressLimit)
            {
                Stun();
            }
            else if (value < 0)
                stressLevel = 0;
        }
    }

    [SerializeField] protected bool GoingBack = false;
    [SerializeField] protected bool CanMove = false;


    [SerializeField] protected float stressLevel = 0f;
    [SerializeField] protected float stressLimit = 1f;
    [SerializeField] protected float stressRate = 0.1f;
    [SerializeField] protected float stressDecreaseRate = 0.001f;
    [SerializeField] protected float stressTime = 0f;
    [SerializeField] protected float swimSpeed = 1f;
    [SerializeField] protected float maxSpeed = 1f;
    [SerializeField] protected float stunTime = 1f;

    [SerializeField] protected float swimmerResultTime = 0f;
    [SerializeField] private Rigidbody rb;


    private void Awake()
    {
        SwimmingController.OnSwimmingGameStarted.AddListener(DecreseStress);
    }

    protected void Push()
    {
        if(CanMove)
        {
            stressTime = stressDecreaseRate;
            rb.AddForce(transform.forward * swimSpeed);
            StressLevel += stressRate;
        }
     
    }

    protected void DecreseStress()
    {
        CanMove = true;
        StartCoroutine(StartDecreseStress());
    }

    private IEnumerator StartDecreseStress()
    {
        stressTime = stressDecreaseRate;
        while(true)
        {
            //Debug.Log("DECRESING");
            StressLevel -= stressTime;
            stressTime  += stressDecreaseRate;
            yield return new WaitForFixedUpdate();
        }
    }

    protected void Stun()
    {
        CanMove = false;
        float tmpspeed = swimSpeed;
        swimSpeed = 0f;
        Invoke(nameof(Recover),stunTime);
        rb.velocity = Vector3.zero;
    }

    private void Recover()
    {
        swimSpeed = maxSpeed;
        CanMove = true;
    }

    protected virtual void TurnAround()
    {
        transform.Rotate(Vector3.up, 180f);
       
        GoingBack = true;
    }

    protected virtual void Finished(float time, LaneController controller)
    {
        SwimmingController.onFinish.Invoke(time, controller);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SwimmingEndPoint"))
        {
            TurnAround();
        }
        else if(GoingBack && other.CompareTag("SwimmingFinish"))
        {
            Debug.Log("Finished");
            Finished(swimmerResultTime,transform.parent.GetComponent<LaneController>());
        }
    }

   
}
