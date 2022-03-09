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

    //PID yPidController = new PID(5f, 2f, 6f);
    //
    //
    //PID xPidController = new PID(3.5f, 1f, 1.5f);
    //PID zPidController = new PID(3.5f, 1f, 1.5f);
    //PID wPidController = new PID(3.5f, 1f, 1.5f);
    
    PID yPidController = new PID(10.05f, 0.19f, 2.37f);


    PID xPidController = new PID(18f, 0.5f, 5.2f);
    PID zPidController = new PID(3.5f, 1f, 1.5f);
    PID wPidController = new PID(4.33f, 0.18f, 1.32f);


    Rigidbody rb;
    Selectable selectable;
    private void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        rb = GetComponent<Rigidbody>();
        selectable = GetComponent<Selectable>();
        PID_ValuesTester.OnxzPidValuesChanged += SetxzPIDValues;
        PID_ValuesTester.OnyPidValuesChanged += SetyPIDValues;
        PID_ValuesTester.OnwPidValuesChanged += SetwPIDValues;

        terrainTransform = FindObjectOfType<DetectorLowersLand>().transform;
        skydiveManager.OnPlaybackStarted += WakeUp;
    }

    private void WakeUp(JumpSequence sequence)
    {
        if(skydiverIndex == 0)
        {
            SetState(new SkydiveState(sequence.DiveFlow[skydiveManager.currentSequenceIndex].BaseOrientation));
        }
        else
        {
            SkydiveFormationSlot slot = sequence.DiveFlow[skydiveManager.currentSequenceIndex].FormationSlots[skydiverIndex-1];
            SetState(new SkydiveState(slot));
        }
        //skydiveManager.getState
    }

    Transform terrainTransform;
    private void SetwPIDValues(Vector3 pid)
    {
        wPidController.Kp = pid.x;
        wPidController.Ki = pid.y;
        wPidController.Kd = pid.z;
    }

    private void SetyPIDValues(Vector3 pid)
    {
        yPidController.Kp = pid.x;
        yPidController.Ki = pid.y;
        yPidController.Kd = pid.z;
    }

    void ResetPIDS()
    {
        PID_ValuesTester tester = FindObjectOfType<PID_ValuesTester>();
        
        yPidController = new PID(tester.yKp, tester.yKi, tester.yKd);
        xPidController = new PID(tester.xzKp, tester.xzKi, tester.xzKd);
        zPidController = new PID(tester.xzKp, tester.xzKi, tester.xzKd);
        wPidController = new PID(tester.wKp, tester.wKi, tester.wKd);
    }

    private void SetxzPIDValues(Vector3 pid)
    {
        xPidController.Kp = pid.x;
        xPidController.Ki = pid.y;
        xPidController.Kd = pid.z;
        zPidController.Kp = pid.x;
        zPidController.Ki = pid.y;
        zPidController.Kd = pid.z;

    }



    SkydiveState currentState;

    public SkydiveState CurrentState { get => currentState; }

    public void SetState(SkydiveState state)
    {
        currentState = state;
        if(currentState.FormationSlot != null)
        {
            target = skydiveManager.SpawnedGhosts[skydiverIndex];
        }
        
        OnButtonPressed?.Invoke((int)state.Orientation);
        ResetPIDS();
        //currentSlot = state.Slot;
    }

    private int GetButtonState()
    {
        return 0;
    }

    public event Action<int> OnButtonPressed;

    float distaceThreshold = 15;

    bool isInFreefall = true;
    bool isSeperating = false;

    float pullHeight = 800;
    float seperationHeight = 1300;

    int skydiverIndex;

    Vector4 GetMovementVector()
    {

        if (terrainTransform.gameObject.activeSelf)
        {

            if (transform.position.y - terrainTransform.position.y < pullHeight && isInFreefall)
            {
                GetComponent<MovementController>().PullParachute();
                isInFreefall = false;

            }

            if (transform.position.y - terrainTransform.position.y < seperationHeight && !isSeperating)
            {
                isSeperating = true;
            }

        }



        Vector4 movement = Vector4.zero;
        if (target == null)
        {
            target = transform ;
        }


        //Vector3 slotOffset = target.TransformDirection(SlotOffset(currentState.Slot));
        Vector3 targetWithSlotOffset = target.position;
        if(currentState != null && currentState.FormationSlot != null)
        {
            //targetWithSlotOffset = target.position;
            //targetWithSlotOffset = target.TransformPoint(SlotPositionHelper.SlotOffset(currentState.Slot));
            if (isSeperating)
            {
                targetWithSlotOffset = (skydiveManager.middlepointNPCS.position- transform.position) * -20;
            }
        }


        
        if (Vector3.Distance(targetWithSlotOffset, transform.position) > distaceThreshold)
        {
            float wPut = Vector3.SignedAngle(transform.forward, targetWithSlotOffset.Flatten() - transform.position.Flatten(), transform.up) / 180;
            //Debug.Log(wPut);
            movement.w = Mathf.Clamp(wPidController.GetOutput(wPut, Time.deltaTime), -1,1);
            movement.z = Mathf.Clamp(Vector3.Distance(targetWithSlotOffset.Flatten(), transform.position.Flatten()) / 5,-.8f,.8f);
            
        }
        else if(currentState != null && currentState.FormationSlot != null && currentState.Slot > 0)
        {

            Vector3 to = transform.InverseTransformDirection(targetWithSlotOffset.Flatten() - transform.position.Flatten());
            
            movement.z = Mathf.Clamp(zPidController.GetOutput(to.z/distaceThreshold, Time.fixedDeltaTime),-.8f,.8f);           
            movement.x = Mathf.Clamp(xPidController.GetOutput(to.x/distaceThreshold, Time.fixedDeltaTime), -.8f, .8f);
            //movement.x = Mathf.Clamp(to.x/(distaceThreshold * .3f), -.95f, .95f);

            if (Vector3.Distance(transform.position.Flatten(),target.position.Flatten()) >.5f)// Vector3.Distance(transform.position.Flatten(), targetWithSlotOffset.Flatten()))
            {
                targetWithSlotOffset.y += .5f;
            }


            //float wPut = Vector3.SignedAngle(transform.forward, target.TransformDirection(Quaternion.AngleAxis(currentState.BaseRotation, Vector3.down)*Vector3.forward), transform.up) / 45;
            float wPut = Vector3.SignedAngle(transform.forward, target.TransformDirection(Vector3.forward), transform.up) / 45;
            //Debug.Log(wPut);
            movement.w = Mathf.Clamp( wPidController.GetOutput(wPut,Time.deltaTime),-1,1);
        }

        movement.y = Mathf.Clamp(-yPidController.GetOutput(transform.position.y - targetWithSlotOffset.y, Time.fixedDeltaTime), -2f, 2f);
        return movement;
    }


    public void SetIndex(int index)
    {
        skydiverIndex = index;
    }
}
