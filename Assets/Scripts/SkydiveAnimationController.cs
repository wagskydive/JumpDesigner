﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkydiveAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rb;

    Vector3 lastPosition;

    [SerializeField]
    IInput input;

    int average = 5;
    Vector3[] PrevPos;
    Vector3 NewPos;

    Quaternion PrevRot;
    Quaternion NewRot;

    //public Vector3 ObjVelocity;
    public Vector3 ObjRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        lastPosition = transform.position;
        PrevPos = new Vector3[average];
        for (int i = 0; i < average; i++)
        {
            PrevPos[i] = transform.position;
        }
        PrevRot = transform.rotation;
        input = GetComponent<IInput>();


    }

    int CurrentAveragInt = 0;




    Vector3 AverageVelocity()
    {
        Vector3 integral = Vector3.zero;
        for (int i = 0; i < average; i++)
        {
            integral += PrevPos[i];
        }
        return integral / average;
    }

    private void FixedUpdate()
    {

        Vector3 velocity = GetVelocity();
        //Vector3 angularVelocity = GetAngularVelocity();
        Vector4 currentInputs = input.MovementVector;
        animator.SetFloat("TurnSpeed", (Vector3.up * currentInputs.w).y*.5f);

        animator.SetLayerWeight(1, (currentInputs.x + currentInputs.z) / 2);
        animator.SetFloat("SidewaysMovement", currentInputs.x * .8f);
        animator.SetFloat("ForwardMovement", currentInputs.z * .8f);
        //animator.SetFloat("Acceleration", velocity.magnitude);

    }


    void IKHandling()
    {

    }

    Vector3 GetAngularVelocity()
    {

        Vector3 angularVelocity = (transform.rotation.eulerAngles - PrevRot.eulerAngles) / Time.fixedDeltaTime;
        //Debug.Log("Vlinear" + ObjVelocity+ "VRot" + angularVelocity);
        PrevRot = transform.rotation;
        return angularVelocity;
    }

    private Vector3 GetVelocity()
    {
        Vector3 ObjVelocity = transform.InverseTransformDirection((transform.position - AverageVelocity()) / Time.fixedDeltaTime);



        PrevPos[CurrentAveragInt] = transform.position;

        CurrentAveragInt++;
        if (CurrentAveragInt > average - 1)
        {
            CurrentAveragInt = 0;
        }
        
        return ObjVelocity;
    }

    
}