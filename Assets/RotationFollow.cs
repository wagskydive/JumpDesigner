using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFollow : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();

    }
    private void Update()
    {
        transform.Rotate(rb.velocity.z, 0, rb.velocity.x);
    }
}
