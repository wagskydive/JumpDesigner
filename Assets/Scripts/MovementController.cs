using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FreefallOrientation
{
    HeadDown,
    Back,
    HeadUp,
    Belly


}

[RequireComponent(typeof(IInput)), RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public event Action<FreefallOrientation> OnTransition;
    public event Action<Vector4> OnMovement;

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



    FreefallOrientation currentOrientation = FreefallOrientation.Belly;



    public void TransitionForward()
    {
        int nextOrentationIndex = (int)currentOrientation + 1; 
        if(nextOrentationIndex > 3) { nextOrentationIndex = 0; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.right);
    }


    public void TransitionBackward()
    {
        int nextOrentationIndex = (int)currentOrientation - 1;
        if (nextOrentationIndex < 0) { nextOrentationIndex = 3; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.left);
    }

    
    public void TransitionRight()
    {
        int nextOrentationIndex = (int)currentOrientation + 2;
        if (nextOrentationIndex > 3 ) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.back,2);
    }


    public void TransitionLeft()
    {
        int nextOrentationIndex = (int)currentOrientation + 2;
        if (nextOrentationIndex > 3) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.forward,2);
    }
    
    public void Turn180Left()
    {
        Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 180);

    }

        
    public void Turn180Right()
    {
        Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), -180);
    }


    void Transition(FreefallOrientation end, Vector3 axis, int repeat = 1)
    {
        Vector3 axisWorld = transform.TransformDirection(axis);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 90 * repeat);
        currentOrientation = end;
        OnTransition?.Invoke(end);
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
        if(inputs != Vector4.zero)
        {
            rb.AddRelativeForce(new Vector3(inputs.x, inputs.y, inputs.z) * movementSpeed);
            rb.AddTorque(Vector3.up * inputs.w * turnSpeed);
            OnMovement?.Invoke(inputs);
        }
        //rb.AddForce(new Vector3(gravity.x, 0, gravity.y) * movementSpeed);

    }
    public void SetOffset(float offset)
    {
        xOffset = offset;
    }

}
