using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurlingController : ActivityControllerBase
{
    public class CurlingLivesChangedEvent : UnityEvent<int> { }
    public static CurlingLivesChangedEvent OnLivesChanged = new CurlingLivesChangedEvent();

    [SerializeField] private Transform curlingHide;
    [SerializeField] private Transform curlingContent;
    [SerializeField] private CurlingCarpetControl carpetControl;
    [SerializeField] private CinemachineVirtualCamera vCamRoll;

    [SerializeField] private GameObject cartPref;
    [SerializeField] private Transform cartHolder;
    [SerializeField] private Transform activeCart;
    [SerializeField] private Rigidbody activeCartRb;

    [SerializeField] private bool ActiveCartPushed = false;
    [SerializeField] private float cartForce = 200f;
    [SerializeField] private int curlingLives = 3;
    [SerializeField] private Transform arrow;

    public int CurlingLives
    {
        get
        {
            return curlingLives;
        }

        set
        {
            if(value!= curlingLives)
                OnLivesChanged.Invoke(value);
            curlingLives = value;
        }
    }

    public override void InitializeActivity()
    {
        curlingContent.gameObject.SetActive(true);
        curlingHide.gameObject.SetActive(false);

        SpawnCart();
        carpetControl.SpawnTargets();
    }

    public override void DeInitializeActivity()
    {
        curlingContent.gameObject.SetActive(false);
        curlingHide.gameObject.SetActive(true);

        carpetControl.CurlingDespawnTargets();
        DespawnCarts();
    }

    private void Start()
    {
        //OnLivesChanged.AddListener(CheckCartRespawn);
    }

    private void Update()
    {
        arrow.LookAt(carpetControl.directionTarget, Vector3.up);

        if (activeCart != null && ActiveCartPushed && activeCartRb.velocity == Vector3.zero)
        {
            CheckCartRespawn(activeCart);
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeCart != null && ActiveCartPushed != true)
            {
                LaunchCart();
            }

        }
    }

    private void LaunchCart()
    {
        CurlingLives--;
        activeCartRb.AddForce(new Vector3(arrow.forward.x, activeCartRb.velocity.y, arrow.forward.z) * cartForce);
        Invoke(nameof(ActiveCartPushInvoker), 0.5f);
        StopAllCoroutines();
    }

    private void ActiveCartPushInvoker()
    {
        ActiveCartPushed = true;
    }
    

    public void CheckCartRespawn(Transform targetCart)
    {
        if(targetCart.GetComponent<CurlingCartControl>().FinalScore==0)
        {
            targetCart.gameObject.SetActive(false);
        }
        else
        {

        }


        if(CurlingLives>0)
        {
            SpawnCart();
        }
        else
        {
            //StartCoroutine(ResultsSequence());
        }
        
    }

    private void SpawnCart()
    {
      
        GameObject tmpCart = Instantiate(cartPref, cartHolder);
        activeCart = tmpCart.transform;
        activeCartRb = activeCart.GetComponent<Rigidbody>();
        ActiveCartPushed = false;
        vCamRoll.Follow = activeCart;
        vCamRoll.LookAt = activeCart;

        StartCoroutine(carpetControl.MoveCarpetTarget());
        
    }

    private void DespawnCarts()
    {
        foreach (Transform item in cartHolder)
        {
            Destroy(item.gameObject);
        }
        CurlingLives = 3;
        ActiveCartPushed = false;
        activeCart = null;
        
    }


}

