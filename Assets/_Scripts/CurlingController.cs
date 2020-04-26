using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurlingController : ActivityControllerBase
{
    public class CurlingLivesChangedEvent : UnityEvent<int> { }
    public static CurlingLivesChangedEvent OnCurlingLivesChanged = new CurlingLivesChangedEvent();

    public class CurlingScoreChangedEvent : UnityEvent<string> { }
    public static CurlingScoreChangedEvent OnCurlingScoreChanged = new CurlingScoreChangedEvent();

    public class CurlingRoundChangedEvent : UnityEvent<string> { }
    public static CurlingRoundChangedEvent OnCurlingRoundChanged = new CurlingRoundChangedEvent();

    [SerializeField] private Transform curlingHide;
    [SerializeField] private Transform curlingContent;
    [SerializeField] private CurlingCarpetControl carpetControl;
    [SerializeField] private CinemachineVirtualCamera vCamRoll;
    [SerializeField] private CinemachineVirtualCamera vCamLaunch;

    [SerializeField] private GameObject cartPref;
    [SerializeField] private Transform cartHolder;
    [SerializeField] private Transform activeCart;
    [SerializeField] private Rigidbody activeCartRb;

    [SerializeField] private bool ActiveCartPushed = false;
    [SerializeField] private bool CanBePushed = false;
    [SerializeField] private float cartForce = 200f;
    [SerializeField] private int curlingLives = 3;
    [SerializeField] private int curlingScore = 0;
    [SerializeField] private int curlingRound = 1;
    [SerializeField] private Transform arrow;

    [SerializeField] private GameObject fltTextPref;

    public int CurlingLives
    {
        get
        {
            return curlingLives;
        }

        set
        {
            if(value!= curlingLives)
                OnCurlingLivesChanged.Invoke(value);
            curlingLives = value;
        }
    }

    public int CurlingScore
    {
        get
        {
            return curlingScore;
        }

        set
        {
            if(value != curlingScore)
            {
                if (value < 0)
                    value = 0;
                curlingScore = value;
                OnCurlingScoreChanged.Invoke(value.ToString());

            }
        }
    }

    public int CurlingRound
    {
        get
        {
            return curlingRound;
        }

        set
        {
            if(curlingRound != value)
                OnCurlingRoundChanged.Invoke(value.ToString());
            curlingRound = value;
            if (curlingRound > 3)
            {
                curlingRound = 3;
               
                StopAllCoroutines();

                ToggleActivityUIForResults();
            }
        }
    }

    public override void InitializeActivity()
    {
        curlingContent.gameObject.SetActive(true);
        curlingHide.gameObject.SetActive(false);

        CheckCartRespawn();
        carpetControl.CurlingSpawnTargets();
    }

    public override void DeInitializeActivity()
    {
        curlingContent.gameObject.SetActive(false);
        curlingHide.gameObject.SetActive(true);


        CurlingScore = 0;
        CurlingRound = 1;

        carpetControl.CurlingDespawnTargets();
        DespawnCarts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ActiveCartPushed != true && CanBePushed)
            {
                LaunchCart();
            }
        }
    }

    private void FixedUpdate()
    {
        arrow.LookAt(carpetControl.directionTarget, Vector3.up);

        if (activeCart != null && CanBePushed)
        {
            if (ActiveCartPushed && activeCartRb.velocity == Vector3.zero)
            {
                CheckCartRespawn();
            }
        }
          
    }

   
    public IEnumerator CurlingCalculateScores()
    {
        
        foreach (Transform cart in cartHolder)
        {
            CurlingResultCalculateSingleCart(cart);   
            yield return new WaitForSecondsRealtime(0.5f);
        }


        foreach (Transform target in carpetControl.TargetsHolder)
        {
            CurlingResultCalculateSingleCart(target);
            yield return new WaitForSecondsRealtime(0.5f);
        }


        NextRound();
    }

    private void NextRound()
    {
        carpetControl.CurlingDespawnTargets();
        DespawnCarts();

        carpetControl.CurlingSpawnTargets();
        CheckCartRespawn();

        CurlingRound++;
        CurlingLives = 3;
    }


    private void CurlingResultCalculateSingleCart(Transform targetCart)
    {
        CurlingCartControl tmpControl = targetCart.GetComponent<CurlingCartControl>();
        if (Mathf.Abs(tmpControl.ReachedTarget) != 0)
        {
            tmpControl.ResultReact();
            GameObject fltText = Instantiate(fltTextPref, targetCart.position, Quaternion.identity, targetCart);
            fltText.GetComponent<FltText>().scoreNumberText.text = string.Format("{0}{1}", (Mathf.Sign(tmpControl.ReachedTarget) < 0 ? "" : "+").ToString(), tmpControl.ReachedTarget.ToString());
        }
        CurlingScore += tmpControl.ReachedTarget;
      
    }

    private void LaunchCart()
    {
        Invoke(nameof(ActiveCartStartDelay), 1f);
        CanBePushed = false; 
        Debug.Log("Pushed");
        CurlingLives--;
        activeCartRb.AddForce(new Vector3(arrow.forward.x, activeCartRb.velocity.y, arrow.forward.z) * cartForce);
        activeCart.LookAt(carpetControl.directionTarget, Vector3.up);

        arrow.gameObject.SetActive(false);
        carpetControl.directionTarget.gameObject.SetActive(false);
        ActiveCartPushed = true;
        StopAllCoroutines();
    }
    
    private void ActiveCartStartDelay()
    {
        CanBePushed = true;
    }

    public void CheckCartRespawn()
    {
        Debug.Log("CHECK " + CanBePushed);
        //if(targetCart != null && targetCart.GetComponent<CurlingCartControl>().ReachedTarget==0)
        //{
        //    StartCoroutine(DisableFailedCart(targetCart));
        //}
       
        if(CurlingRound<=3)
        {
            if(CurlingLives>0)
            {
                SpawnCart();
            }
            else
            {
                activeCart = null;
                StartCoroutine(CurlingCalculateScores());
            }
        }

    }

    private void SpawnCart()
    {
        Invoke(nameof(ActiveCartStartDelay), 0.5f);
        GameObject tmpCart = Instantiate(cartPref, cartHolder);
        activeCart = tmpCart.transform;
        activeCartRb = activeCart.GetComponent<Rigidbody>();
        ActiveCartPushed = false;
        vCamRoll.Follow = activeCart;
        vCamRoll.LookAt = activeCart;

        arrow.gameObject.SetActive(true);
        carpetControl.directionTarget.gameObject.SetActive(true);
        StartCoroutine(carpetControl.MoveCarpetTarget());
        //CameraManager.Instance.SetLive(vCamLaunch);
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

