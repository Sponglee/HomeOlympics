

using UnityEngine;

public class BasketBallInputController : MonoBehaviour
{
    [SerializeField] private BasketBallController basketController;
    public float cameraOffset = 1f;
    public float rotationSpeed = 1f;
    public Transform directionTarget;
    public Transform arrow;

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = cameraOffset;
        directionTarget.position = Camera.main.ScreenToWorldPoint(temp) - Vector3.up*0.1f;

        transform.LookAt(directionTarget, Vector3.up);


        UpdateArrowSize();
    }

    private void UpdateArrowSize()
    {
        arrow.localScale = Vector3.one * basketController.currentSpeed / basketController.speedRange.y + Vector3.one*0.3f;
    }
}
