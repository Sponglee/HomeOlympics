
using UnityEngine;

public class SwimmingWaveMover : MonoBehaviour
{
    public Transform[] waves;
    public Transform waveRespawn;
    public float waveSpeed = 1f;

    private void FixedUpdate()
    {
        foreach(Transform wave in waves)
        {
            wave.Translate(Vector3.left * waveSpeed);

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SwimmingWave"))
        {
            other.transform.parent.position = waveRespawn.position;
        }
    }
}
