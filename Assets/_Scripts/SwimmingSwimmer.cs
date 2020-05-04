using System.Collections;


using UnityEngine;

public class SwimmingSwimmer : MonoBehaviour
{
    public Animator animator;
    public Sprite flag;
  

    public virtual float SwimmerResultTime
    {
        get
        {
            return swimmerResultTime;
        }

        set
        {

            swimmerResultTime = value;
            
        }
    }

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

    private float swimmerResultTime = 0;

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

    [SerializeField] private Rigidbody rb;


    private void Awake()
    {
        SwimmingController.OnSwimmingGameStarted.AddListener(StartStress);
      
    }

    protected virtual void Push(float targetSpeed)
    {
        if(CanMove)
        {
           
            stressTime = stressDecreaseRate;
            rb.AddForce(transform.forward * targetSpeed);
            StressLevel += stressRate;
        }
        animator.SetTrigger("walk");
    }

    protected void StartStress()
    {
        SwimmerResultTime = 0;
        CanMove = true;
        StartCoroutine(StartDecreseStress());
    }

    private IEnumerator StartDecreseStress()
    {
        stressTime = stressDecreaseRate;
        while(true)
        {
            //Debug.Log("DECRESING");
            yield return new WaitForFixedUpdate();
            StressLevel -= stressTime;
            stressTime  += stressDecreaseRate;
            SwimmerResultTime += Time.deltaTime;
        }
    }

    protected void Stun()
    {
        CanMove = false;
        float tmpspeed = swimSpeed;
        swimSpeed = 0f;
        Invoke(nameof(Recover),stunTime);
        rb.velocity = Vector3.zero;
        animator.SetTrigger("stun");
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
        StopAllCoroutines();
      
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
            Finished(SwimmerResultTime,transform.parent.GetComponent<LaneController>());
        }
    }

   
}
