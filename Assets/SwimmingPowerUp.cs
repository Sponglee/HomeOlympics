using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwimmingPowerUp : MonoBehaviour
{
    public class SwimmingPowerUpLevel : UnityEvent<float> { }
    public static SwimmingPowerUpLevel OnSwimmingPowerChanged = new SwimmingPowerUpLevel();

    public Image img;
    [SerializeField] private float poweredUp = 0;
    public float PoweredUp
    {
        get
        {
            return poweredUp;
        }

        set
        {
            poweredUp = value;

            OnSwimmingPowerChanged.Invoke(value);

            if (value >0)
            {
                img.color = Color.red;
            }
            else
            {
                img.color = Color.white;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp1"))
        {
            PoweredUp = 1f;
            
        }
        else if (other.CompareTag("PowerUp2"))
        {
            PoweredUp = 1.5f;
        }
        else if (other.CompareTag("PowerUp3"))
        {
            PoweredUp = 3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PowerUp1") || other.CompareTag("PowerUp2") || other.CompareTag("PowerUp3"))
        {
            PoweredUp = 0;
        }
    }
}
