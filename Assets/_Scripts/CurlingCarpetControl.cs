using System.Collections;

using UnityEngine;


public class CurlingCarpetControl : MonoBehaviour
{
    //public class CarpetEnteredEvent : UnityEvent<Transform> { }
    //public static CarpetEnteredEvent OnCarpetEntered = new CarpetEnteredEvent();

    public Transform directionTarget;
    public Transform TargetsHolder{get{return targetsHolder;}}

    [SerializeField] private Transform cartsHolder;
    [SerializeField] private Transform targetsHolder;
    [SerializeField] private GameObject targetPref;
    [SerializeField] private float carpetRadius = 1.5f;

    public void CurlingSpawnTargets()
    {
        
        for (int i = 0; i < 3; i++)
        {

            float a = 360 / 3 * i;

            Vector3 pos = RandomCircle(transform.position, Random.Range(0f,carpetRadius), a);
            
            Instantiate(targetPref, pos, Quaternion.identity, targetsHolder);
        }
    }
  
    public void CurlingDespawnTargets()
    {
        foreach (Transform item in targetsHolder)
        {
            Destroy(item.gameObject);
        }
    }

    //Build circle for spots
    public Vector3 RandomCircle(Vector3 center, float radius, float a)
    {
        //Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public IEnumerator MoveCarpetTarget()
    {
        float moveLength = carpetRadius * 2f + 1f;
        while (true)
        {
            directionTarget.localPosition = new Vector3(Mathf.PingPong(Time.time * moveLength, moveLength) - moveLength / 2f, 0f, 0f);
            yield return new WaitForFixedUpdate();
        }
    }
}
