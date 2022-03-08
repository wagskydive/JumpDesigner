using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class SkydiveAnimationController : MonoBehaviour
{
    

    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rb;

    Vector3 lastPosition;

    [SerializeField]
    MovementController movementController;

    int average = 5;
    Vector3[] PrevPos;
    Vector3 NewPos;

    Quaternion PrevRot;
    Quaternion NewRot;

    //public Vector3 ObjVelocity;
    public Vector3 ObjRotation;


    float transitionCompletionFactor = 0;
    bool transitioning = false;

    float orientationFloatGoal;
    float lastOrientation;
    int lastOrientationInt;
    float difference;

    bool overflow;
    [SerializeField]
    TwoBoneIKConstraint leftLegIK;

    [SerializeField]
    TwoBoneIKConstraint rightLegIK;

    [SerializeField]
    Transform ChestMovementHandler;


    [SerializeField]
    Transform ChestRotationHandler;

    [SerializeField]
    ChainIKConstraint spineRotationIK;
    
    [SerializeField]
     ChainIKConstraint spineIK;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        movementController.OnPull += PullAnimation;
        lastPosition = transform.position;
        PrevPos = new Vector3[average];
        for (int i = 0; i < average; i++)
        {
            PrevPos[i] = transform.position;
        }
        PrevRot = transform.rotation;

        movementController.OnMovement += Movement;

        movementController.OnTransition += Transition;
        lastOrientationInt = (int)movementController.CurrentOrientation;
    }

    private void PullAnimation()
    {
        animator.SetBool("Pull", true);
    }

    private void Transition(FreefallOrientation orientation, float xRotation)
    {
        animator.SetFloat("Orientation", (int)orientation);

        //currentRotation = xRotation;
        //int orientationInt = (int)orientation;
        //if(orientationInt == 0)
        //{
        //    if(lastOrientationInt == 3)
        //    {
        //        orientationInt = 4;
        //        overflow = true;
        //    }
        //}
        //
        //if (orientationInt == 3)
        //{
        //    if (lastOrientationInt == 0)
        //    {
        //        animator.SetFloat("Orientation", 4);
        //    }
        //}
        //orientationFloatGoal = orientationInt;
        //lastOrientation = animator.GetFloat("Orientation");
        //
        //
        //animator.SetFloat("Orientation", (int)orientation);
        //
        //difference = orientationFloatGoal - lastOrientation;
        //transitioning = true;
        //
        //
        //lastOrientationInt = orientationInt;
    }


    float currentRotation;
    private void FixedUpdate()
    {
        //if (transitioning)
        //{
        //    transitionCompletionFactor += Time.deltaTime;
        //
        //    float progress = transitionCompletionFactor / movementController.TransitionSpeed;
        //
        //    animator.SetFloat("Orientation",lastOrientation+(difference* progress) );
        //    //
        //    if (progress >= 1)
        //    {
        //        if (overflow)
        //        {
        //            
        //            animator.SetFloat("Orientation", 0);
        //            overflow = false;
        //        }
        //        transitioning = false;
        //        progress = 0;
        //        transitionCompletionFactor = 0;
        //    }
        //}



        
    }
    float OrientationFloatToDegrees()
    {
        float orientFloat = (animator.GetFloat("Orientation")*90)-180;


        return orientFloat;
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

    private void Movement(Vector3 translation, float steering)
    {
        Movement(new Vector4(translation.x, translation.y, translation.z, steering));
    }

    private void Movement(Vector4 currentInputs)
    {

        animator.SetFloat("ForwardSpeed", currentInputs.z * .8f);
        animator.SetLayerWeight(1, Mathf.Abs(currentInputs.z * .6f));
        //animator.SetFloat("TurnSpeed", (Vector3.up * currentInputs.w).y*.5f);

        

        animator.SetFloat("SidewaysMovement", currentInputs.x * .8f);

        int headUpOrDown = 1;

        float orientationBend = currentInputs.z;

        if(orientationBend < 0)
        {
            if (orientationBend > -.8f)
            {
                orientationBend = 0;
            }
            else
            {
                orientationBend = (orientationBend + .8f) * 5;
            }


        }
        else
        {
            if (orientationBend < .8f)
            {
                orientationBend = 0;
            }
            else
            {
                orientationBend = (orientationBend - .8f) * 5;
            }

        }

        if (movementController.CurrentOrientation == FreefallOrientation.HeadDown || movementController.CurrentOrientation == FreefallOrientation.HeadUp)
        {
            headUpOrDown = -1;
            animator.SetFloat("Orientation", (int)movementController.CurrentOrientation - orientationBend * .5f);
        }
        else
        {
            animator.SetFloat("Orientation", (int)movementController.CurrentOrientation + orientationBend * .5f);
        }

        ChestMovementHandler.transform.localPosition = new Vector3(0, 1, (currentInputs.z-orientationBend) * movementController.controlMode * headUpOrDown);

        movementController.CharacterOffset.localEulerAngles = new Vector3(OrientationFloatToDegrees(), 0, 0);


        //leftLegIK.weight = Mathf.Clamp(currentInputs.x, -1, 0)*-1;
        //rightLegIK.weight = Mathf.Clamp(currentInputs.x, 0, 1);

        spineIK.weight = Mathf.Abs(currentInputs.z);


        float spineX = currentInputs.x * 35;
        if (movementController.CurrentOrientation == FreefallOrientation.Back)
        {
            spineX *= -1;
        }
        

        spineRotationIK.weight = Mathf.Abs(currentInputs.w + currentInputs.x*.3f);

        ChestRotationHandler.transform.localEulerAngles = new Vector3(0, (-currentInputs.w * headUpOrDown * 65) - spineX, 0); //localPosition = new Vector3(currentInputs.w, 1, 0);// * movementController.controlMode * headUpOrDown);    //.LookAt(transform.right, ChestRotationHandler.up);// Rotate(new Vector3(0,currentInputs.w, 0),.1f);//.localRotation.se.SetLookRotation(new Vector3(currentInputs.w, 1, 1), ChestRotationHandler.up);

        animator.SetFloat("Acceleration", -currentInputs.y * .8f);

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
