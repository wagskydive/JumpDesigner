using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : MonoBehaviour
{
    public event Action<Collision> OnImpact;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float forceThreshold = 4;

    private void OnCollisionEnter(Collision collision)
    {
        float force = collision.relativeVelocity.magnitude;
        Debug.Log(force.ToString());
        if(force > forceThreshold)
        {

            
            OnImpact?.Invoke(collision);
        }
    }
}
