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

    public bool TransitionPossible
    {
        get => transitionTimer == 0;
    }
    [SerializeField]
    private float transitionSpeed = 2;

    public float TransitionSpeed { get => transitionSpeed;}


    float transitionTimer = 0;

    public FreefallOrientation CurrentOrientation = FreefallOrientation.Belly;

    public int controlMode { get; private set; }

    public void TransitionForward()
    {
        int nextOrentationIndex = (int)CurrentOrientation + controlMode; 
        if(nextOrentationIndex > 3) { nextOrentationIndex = 0; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.right);
    }


    public void TransitionBackward()
    {
        int nextOrentationIndex = (int)CurrentOrientation - controlMode;
        if (nextOrentationIndex < 0) { nextOrentationIndex = 3; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.left);
    }

    
    public void TransitionRight()
    {
        int nextOrentationIndex = (int)CurrentOrientation + 2;
        if (nextOrentationIndex > 3 ) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.back,2);
        controlMode *= -1;
    }


    public void TransitionLeft()
    {
        int nextOrentationIndex = (int)CurrentOrientation + 2;
        if (nextOrentationIndex > 3) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex, Vector3.forward,2);
        controlMode *= -1;
    }
    
    public void Turn180Left()
    {
        Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 180);
        controlMode *= -1;

    }

    public void Turn180Right()
    {
        Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), -180);
        controlMode *= -1;
    }


    void HandleTransitionTimer()
    {
        if(transitionTimer > 0)
        {
            transitionTimer -= Time.fixedDeltaTime;
        }

        if (transitionTimer < 0)
        {
            transitionTimer = 0;
        }
    }
        


    void Transition(FreefallOrientation end, Vector3 axis, int repeat = 1)
    {
        transitionTimer = transitionSpeed;
        Vector3 axisWorld = transform.TransformDirection(axis);
        CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 90 * repeat);
        CurrentOrientation = end;


        OnTransition?.Invoke(end);

    }

    private void Awake()
    {
        input = GetComponent<IInput>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        controlMode = 1;
    }

    
    float ForwardSign(FreefallOrientation orientation)
    {
        int cur = 1;
        if(orientation == FreefallOrientation.HeadUp || orientation == FreefallOrientation.HeadDown)
        {
            cur =  -1;
        }
        return cur* controlMode;
        
    }




    private void FixedUpdate()
    {
        
        HandleTransitionTimer();
        Vector4 inputs = input.MovementVector;
        if(inputs != Vector4.zero)
        {
            Vector4 movementVectorAdjusted = new Vector4(inputs.x, inputs.y, inputs.z, inputs.w);

            rb.AddRelativeForce(movementVectorAdjusted * movementSpeed);


            rb.AddTorque(Vector3.up * movementVectorAdjusted.w * turnSpeed);

            OnMovement?.Invoke(movementVectorAdjusted);


            if (TransitionPossible)
            {
                if (inputs.z == 1 && inputs.y == -1)
                {
                    TransitionForward();
                }
                if (inputs.z == -1 && inputs.y == 1)
                {
                    TransitionBackward();
                }
            }

        }
        //rb.AddForce(new Vector3(gravity.x, 0, gravity.y) * movementSpeed);

    }
    public void SetOffset(float offset)
    {
        xOffset = offset;
    }

}
