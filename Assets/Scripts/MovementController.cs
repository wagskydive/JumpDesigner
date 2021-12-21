using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FreefallOrientation
{
    Belly = 90,
    HeadDown = 180,
    Back = 270,
    HeadUp = 0

}

[RequireComponent(typeof(IInput)), RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Transform CharacterOffset;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed = 2f;
    [SerializeField]
    public float xOffset = 0;


    [SerializeField]
    private IInput input;

    Gyroscope gyro;

    void Transition(FreefallOrientation end, int type)
    {
        Vector3 endRotation = CharacterOffset.transform.localRotation.eulerAngles;
        endRotation.x = (float)end;
        CharacterOffset.transform.Rotate(CharacterOffset.transform.right,90);
    }

    private void Awake()
    {
        input = GetComponent<IInput>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;

    }
    private void FixedUpdate()
    {
        Vector4 inputs = input.MovementVector;

        //rb.AddForce(new Vector3(gravity.x, 0, gravity.y) * movementSpeed);
        rb.AddRelativeForce(new Vector3(inputs.x, inputs.y, inputs.z) * movementSpeed);
        rb.AddTorque(Vector3.up * inputs.w * turnSpeed);
    }
    public void SetOffset(float offset)
    {
        xOffset = offset;
    }

}
