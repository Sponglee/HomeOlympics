using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlingQuadFrictionControll : MonoBehaviour
{
    [SerializeField] private PhysicMaterial frictionMat;
    [SerializeField] private PhysicMaterial noFrictionMat;
    [SerializeField] private BoxCollider collider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CurlingBroom"))
        {
            collider.material = noFrictionMat;
            Invoke(nameof(SetFrictionBack), 3f);
        }
    }

    private void SetFrictionBack()
    {
        collider.material = frictionMat;
    }

}
