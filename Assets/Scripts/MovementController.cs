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

public class ForwardTransition : FreefallTansition
{
    public ForwardTransition(FreefallOrientation endOrientation, Transform offsetTransform) : base(endOrientation, offsetTransform)
    {
    }

    public override void Transition(Transform offsetTransform)
    {
        offsetTransform.Rotate(Vector3.right, 90);

    }
}

public abstract class FreefallTansition 
{
    public event Action<FreefallOrientation> OnTransition;

    public FreefallTansition(FreefallOrientation endOrientation, Transform offsetTransform)
    {
        _endOrientation = endOrientation;
        Transition(offsetTransform);
        OnTransition?.Invoke(endOrientation);
    }

    FreefallOrientation _endOrientation;

    public abstract void Transition(Transform offsetTransform);

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



    public void TestBellyToHeadDownTransition()
    {
        Transition(FreefallOrientation.HeadDown, Vector3.right);
    }


    void Transition(FreefallOrientation end, Vector3 axis)
    {

        CharacterOffset.transform.Rotate(axis, 90);
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
