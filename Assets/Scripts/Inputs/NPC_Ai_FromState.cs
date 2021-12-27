using System;
using UnityEngine;

public static class ExtentionMethods
{
    public static Vector3 Flatten(this Vector3 vector3)
    {
        return new Vector3(vector3.x, 0, vector3.z);
    }

    public static Vector3 OnlyY(this Vector3 vector3)
    {
        return new Vector3(0, vector3.y, 0);
    }
}

public class PID
{
    private float _kP, _kI, _kD;


    public float Kp
    {
        get
        {
            return _kP;
        }
        set
        {
            _kP = value;
        }
    }

    public float Ki
    {
        get
        {
            return _kI;
        }
        set
        {
            _kI = value;
        }
    }


    public float Kd
    {
        get
        {
            return _kD;
        }
        set
        {
            _kD = value;
        }
    }


    private float _p, _i, _d;
    private float _previousError;


    public PID(float p,float i,float d)
    {
        _kP = p;
        _kI = i;
        _kD = d;
    }

    public float GetOutput(float currentError, float deltaTime)
    {
        _p = currentError;
        _i += _p * deltaTime;
        _d = (_p - _previousError) / deltaTime;
        _previousError = currentError;
        return _p * Kp + _i * Ki + _d * Kd;
    }
}


public class NPC_Ai_FromState : MonoBehaviour, IInput
{
    public Transform target;

    //public int currentSlot;

    public Vector4 MovementVector => GetMovementVector();

    public int CurrentButtonsState => GetButtonState();

    SkydiveManager skydiveManager;

    PID xPidController = new PID(.4f, .2f, .3f);


    Rigidbody rb;
    Selectable selectable;
    private void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        rb = GetComponent<Rigidbody>();
        selectable = GetComponent<Selectable>();

    }

    Vector3 SlotOffset(int slot)
    {
        if(slot == 0)
        {
            return Vector3.zero;
        }

        if (slot == 1)
        {
            return new Vector3(-1, 0, 1);
        }



        if (slot == 2)
        {
            return new Vector3(0, 0, 1);
        }


        if (slot == 3)
        {
            return new Vector3(1, 0, 1);
        }


        if (slot == 4)
        {
            return new Vector3(-1, 0, 0);
        }



        if (slot == 5)
        {
            return new Vector3(1, 0, 0);
        }


        if (slot == 6)
        {
            return new Vector3(-1, 0, -1);
        }


        if (slot == 7)
        {
            return new Vector3(0, 0, -1);
        }


        if (slot == 8)
        {
            return new Vector3(1, 0, -1);
        }
        else
        {
            return Vector3.zero;
        }
    }

    SkydiveState currentState;

    public void SetState(SkydiveState state)
    {
        currentState = state;
        target = currentState.Target.transform;
        //currentSlot = state.Slot;
    }

    private int GetButtonState()
    {
        return 0;
    }

    public event Action<int> OnButtonPressed;

    float distaceThreshold = 3;
    Vector4 GetMovementVector()
    {
        Vector4 movement = Vector4.zero;
        if (target == null)
        {
            target = skydiveManager.middlepointNPCS;
        }


        //Vector3 slotOffset = target.TransformDirection(SlotOffset(currentState.Slot));
        Vector3 targetWithSlotOffset = target.position;
        if(currentState != null)
        {
            targetWithSlotOffset = target.TransformPoint(SlotOffset(currentState.Slot));
        }


        
        if (Vector3.Distance(targetWithSlotOffset, transform.position) > distaceThreshold)
        {
            float wPut = Vector3.SignedAngle(transform.forward, targetWithSlotOffset.Flatten() - transform.position.Flatten(), transform.up) / 180;
            Debug.Log(wPut);
           movement.w = Mathf.Clamp(wPut,-1,1);
            movement.z = Mathf.Clamp(Vector3.Distance(targetWithSlotOffset.Flatten(), transform.position.Flatten()) / 5,-.95f,.95f);
            
        }
        else if(currentState != null && currentState.Slot > 0)
        {

            Vector3 to = transform.InverseTransformDirection(targetWithSlotOffset.Flatten() - transform.position.Flatten());
            
            movement.z = Mathf.Clamp(to.z/(distaceThreshold*.3f), -.95f, .95f);           
            movement.x = Mathf.Clamp(to.x/(distaceThreshold * .3f), -.95f, .95f);

            if(Vector3.Distance(transform.position.Flatten(),target.position.Flatten()) < Vector3.Distance(transform.position.Flatten(), targetWithSlotOffset.Flatten()))
            {
                targetWithSlotOffset.y += .5f;
            }


            float wPut = Vector3.SignedAngle(transform.forward, target.forward, transform.up) / 45;
            Debug.Log(wPut);
            movement.w = Mathf.Clamp(wPut, -.95f, .95f);
        }

        movement.y = -Mathf.Clamp(transform.position.y - targetWithSlotOffset.y, -.95f, .95f);
        return movement;
    }
}

public class SkydiveState
{
    public SkydiveState(ISelectable target, int slot = 0, Grip leftGrip = null, Grip rightGrip = null)
    {
        LeftGrip = leftGrip;
        RightGrip = rightGrip;
        Target = target;
        Slot = slot;

    }

    public Grip LeftGrip { get; private set; }
    public Grip RightGrip { get; private set; }
    public ISelectable Target { get; private set; }
    public int Slot { get; private set; }

}