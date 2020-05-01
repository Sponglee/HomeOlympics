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

    [SerializeField] private Rigidbody rb;
    

    [SerializeField] protected float stressLevel = 0f;
    [SerializeField] protected float stressLimit = 1f;
    [SerializeField] protected float stressRate = 0.1f;
    [SerializeField] protected float stressDecreaseRate = 0.001f;
    [SerializeField] protected float swimSpeed = 1f;
    [SerializeField] protected float maxSpeed = 1f;
    [SerializeField] protected float stunTime = 1f;
    private void Awake()
    {
        SwimmingController.OnSwimmingGameStarted.AddListener(DecreseStress);
    }

    public void Push()
    {
        rb.AddForce(transform.forward * swimSpeed);
        StressLevel+= stressRate;
    }

    public void DecreseStress()
    {
        StartCoroutine(StartDecreseStress());
    }

    private IEnumerator StartDecreseStress()
    {
        while(true)
        {
            Debug.Log("DECRESING");
            StressLevel -= stressDecreaseRate;
            yield return new WaitForFixedUpdate();
        }
    }
    public void Stun()
    {
        float tmpspeed = swimSpeed;
        swimSpeed = 0f;
        Invoke(nameof(Recover),stunTime);
        
    }

    private void Recover()
    {
        swimSpeed = maxSpeed;
    }
}
