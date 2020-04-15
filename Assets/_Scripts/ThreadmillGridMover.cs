using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadmillGridMover : MonoBehaviour
{
    [SerializeField] private ThreadmillController threadmillController;
    public Material threadmillGrid;

    // Update is called once per frame
    void Update()
    {
        threadmillGrid.mainTextureOffset += new Vector2(threadmillController.threadmillSpeed * Time.deltaTime,0);
    }
}
