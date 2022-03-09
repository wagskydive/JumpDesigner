using System;
using UnityEngine;

public class CanopyAiController : MonoBehaviour, IInput
{
    PID wPidController = new PID(.5f, .6f, 1);

    PID_ValuesTester tester;

    private void SetwPIDValues(Vector3 pid)
    {
        ResetPIDS();

    }

    private void Start()
    {
        PID_ValuesTester.OnCanopywPidValuesChanged += SetwPIDValues;
        tester = FindObjectOfType<PID_ValuesTester>();
        
    }


    void ResetPIDS()
    {
        


        wPidController = new PID(tester.CanopywKp, tester.CanopywKi, tester.CanopywKd);
    }


    public Vector4 MovementVector => GetMovementInput();

    Vector3 target;

    float distaceThreshold = 8;

    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }
    Transform canopyTransform;
    public void SetCanopyTransform(Transform canopy)
    {
        canopyTransform = canopy;
    }


    private Vector4 GetMovementInput()
    {
        Vector4 movement = new Vector4();

        if (canopyTransform != null)
        {



            if (Vector3.Distance(target, canopyTransform.position) > distaceThreshold)
            {


                float wPut = Vector3.SignedAngle(canopyTransform.forward, target.Flatten() - canopyTransform.position.Flatten(), canopyTransform.up) / 180;
                float pidAdjusted = wPidController.GetOutput(wPut, Time.deltaTime);
                if (Mathf.Abs(wPut) > .7f)
                {
                    ResetPIDS();
                }


                if (Mathf.Abs(wPut) > .15f)
                {

                    

                    Debug.Log("wPut: " + wPut + " " + gameObject.name + " pid adjusted input: " + pidAdjusted);
                    //movement.z = Mathf.Clamp(pidAdjusted, 0, 1) * -1;
                    //movement.y = Mathf.Clamp(pidAdjusted, -1, 0);
                    movement.w = Mathf.Clamp(pidAdjusted, -1, 1);

                }
                //Debug.Log(wPut);


            }
        }
        return movement;
    }

    public int CurrentButtonsState => GetButtonState();

    private int GetButtonState()
    {
        throw new NotImplementedException();
    }

    public event Action<int> OnButtonPressed;
}
