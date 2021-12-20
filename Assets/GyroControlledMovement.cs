using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GyroControlledMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    private Transform gyroFollower;

    [SerializeField]
    private float movementSpeed = .2f;
    [SerializeField]
    private float turnSpeed = 2f;
    [SerializeField]
    public float xOffset=0;



    Gyroscope gyro;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        
    }
    private void FixedUpdate()
    {
        gyro = GyroManager.Instance.GetGyro();
        Vector3 gravity = gyro.gravity;
        Debug.Log(gravity);
        rb.AddForce(new Vector3(gravity.x, 0, gravity.y) * movementSpeed);
        //rb.AddTorque(Vector3.up * joystickR.Horizontal * turnSpeed);
    }
    public void SetOffset(float offset)
    {
        xOffset = offset;
    }

}
