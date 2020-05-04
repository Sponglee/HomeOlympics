
using UnityEngine;
using UnityEngine.Video;

public class BedController : ActivityControllerBase
{
    public VideoClip[] channels;
    public VideoPlayer player;

    public Transform remote;
    public Transform hide;

    public float followSpeed = 1f;

    [SerializeField] private int lastChannel = 0;
    public int LastChannel
    {
        get
        {
            return lastChannel;
        }

        set
        {
            if (value > channels.Length - 1)
                value = 0;
            lastChannel = value;
        }
    }

    

    private void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 1.50f; 
        remote.position = Camera.main.ScreenToWorldPoint(temp);


        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySound("channel_switch");
            LastChannel++;
            player.clip = channels[LastChannel];
        }
    }
    public override void DeInitializeActivity()
    {
        
        hide.gameObject.SetActive(true);
        remote.gameObject.SetActive(false);

        

        base.DeInitializeActivity();
    }

    public override void InitializeActivity()
    {
        Cursor.visible = false;
        remote.gameObject.SetActive(true);
        hide.gameObject.SetActive(false);
       
        base.InitializeActivity();
    }
}
