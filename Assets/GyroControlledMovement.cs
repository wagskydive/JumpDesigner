using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControlledMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private Transform gyroFollower;

    [SerializeField]
    private float movementSpeed = .2f;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void FixedUpdate()
    {
        Vector3 gravity = GyroManager.Instance.GetGyro().gravity;
        rb.AddForce(new Vector3(gravity.x,0, gravity.y) * movementSpeed);
    }

}
