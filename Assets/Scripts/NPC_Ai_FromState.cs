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


public class NPC_Ai_FromState : MonoBehaviour, IInput
{
    public Transform target;

    public Vector4 MovementVector => GetMovementVector();

    public int CurrentButtonsState => GetButtonState();

    SkydiveManager skydiveManager;

    Rigidbody rb;
    Selectable selectable;
    private void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        rb = GetComponent<Rigidbody>();
        selectable = GetComponent<Selectable>();

    }


    SkydiveState currentState;

    public void SetState(SkydiveState selectable)
    {
        currentState = selectable;
        target = currentState.Target.transform;
    }

    private int GetButtonState()
    {
        return 0;
    }

    public event Action<int> OnButtonPressed;


    Vector4 GetMovementVector()
    {
        Vector4 movement = Vector4.zero;
        if (target == null)
        {
            target = skydiveManager.middlepointNPCS;
        }

        if (Vector3.Distance(target.position, transform.position) > 1.5f)
        {
            if(target.position.y - transform.position.y < -20)
            {
                OnButtonPressed?.Invoke(10);
            }
            if (target.position.y - transform.position.y > 20)
            {
                OnButtonPressed?.Invoke(7);
            }
            movement.w = Mathf.Clamp(Vector3.SignedAngle(transform.forward, target.position.Flatten() - transform.position.Flatten(),transform.up)/180,-1,1);
            movement.z = Mathf.Clamp(Vector3.Distance(target.position.Flatten(), transform.position.Flatten()) / 5,-.95f,.95f);
            movement.y = -Mathf.Clamp(transform.position.y- target.position.y, -.95f,.95f);
        }


        return movement;
    }
}

public class SkydiveState
{
    public SkydiveState(ISelectable target, int location = 0, Grip leftGrip = null, Grip rightGrip = null)
    {
        LeftGrip = leftGrip;
        RightGrip = rightGrip;
        Target = target;
        Location = location;

    }

    public Grip LeftGrip { get; private set; }
    public Grip RightGrip { get; private set; }
    public ISelectable Target { get; private set; }
    public int Location { get; private set; }

}