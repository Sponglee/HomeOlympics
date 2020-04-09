using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollText : MonoBehaviour
{
    [SerializeField] private float resetCoord;
    [SerializeField] private float speed = 50f;
    [SerializeField] private RectTransform scrollTextRect;
    // Start is called before the first frame update
    void Start()
    {
        scrollTextRect = gameObject.GetComponent<RectTransform>();
        resetCoord = scrollTextRect.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
      
            Debug.Log(Mathf.Abs(scrollTextRect.position.x) + ";" +  resetCoord);
        if (Mathf.Abs(scrollTextRect.position.x) >= resetCoord)
        {
            scrollTextRect.localPosition = Vector3.zero;
        }
    }
}
