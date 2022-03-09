using System;
using System.Collections.Generic;
using UnityEngine;



public class CanopyController : MonoBehaviour
{
    public event Action<Vector4> OnMovement;
    public event Action OnPull;
    public event Action<CanopyController> OnParachuteDeployed;
    public event Action OnCutaway;

    [SerializeField]
    public IInput inputSource;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    private float movementSpeed;
    
    [SerializeField]
    private float canopyForwardSpeed;

    [SerializeField]
    private float canopyDownwardSpeed;

    [SerializeField]
    private float turnSpeed = 4f;

    CanopyConnector canopyConnector;

    public Rigidbody canopyRb;

    public void Pull()
    {

        OnPull?.Invoke();
        Invoke("DeployParachute", 1.4f);
    }

    public void DeployParachute()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/ValkyrieTest")) as GameObject;
        go.transform.position = transform.position;
        canopyConnector = go.GetComponent<CanopyConnector>();
        canopyConnector.ConnectCanopy(this);
        canopyRb = canopyConnector.GetComponent<Rigidbody>();
        go.transform.localScale = Vector3.one * .4f;
        //midSurface.SetFlapAngle(.2f);
        LeanTween.scale(go, Vector3.one, 2);

        rightZPos = rightSideSurface.transform.localPosition.z;
        leftZPos = leftSideSurface.transform.localPosition.z;
        brakesReleased = false;
        OnParachuteDeployed?.Invoke(this);
    }

    public void Cutaway()
    {
        if(canopyConnector != null)
        {
            canopyConnector.CutawayCanopy();
            GameObject go = canopyConnector.gameObject;
            canopyConnector = null;
            SetControls(null, null, null);
            Destroy(go);
            OnCutaway?.Invoke();
        }
    }

    AeroSurface leftSideSurface;
    AeroSurface rightSideSurface;
    AeroSurface midSurface;

    public void SetControls(AeroSurface left,AeroSurface right, AeroSurface mid)
    {
        leftSideSurface = left;
        rightSideSurface = right;
        midSurface = mid;
    }


    public void ReplaceInput(IInput newInput)
    {
        inputSource = newInput;
    }


    Vector4 lastMovemntInputs = Vector4.zero;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        rb.mass = 80;
        rb.drag = 0;


       
    }

    bool brakesReleased;

    float rightZPos;
    float leftZPos;

    float riserSensitivity = 2;

    void SetZPosLeft(float value)
    {
        leftSideSurface.transform.localPosition = new Vector3(leftSideSurface.transform.localPosition.x, leftSideSurface.transform.localPosition.y, leftZPos - value*riserSensitivity);
    }
    
    void SetZPosRight(float value)
    {
        rightSideSurface.transform.localPosition = new Vector3(rightSideSurface.transform.localPosition.x, rightSideSurface.transform.localPosition.y, rightZPos - value * riserSensitivity);
    }

    void FixedUpdate()
    {

        if (inputSource == null)
        {
            if (GetComponent<IInput>() != null)
            {
                ReplaceInput(GetComponent<IInput>());
            }

        }

        Vector3 movementVector = new Vector3(0, -canopyDownwardSpeed, canopyForwardSpeed);


        rb.AddRelativeForce(transform.forward * movementSpeed);
        if (inputSource != null)
        {
            Vector4 movementInputs = inputSource.MovementVector;
            if (movementInputs != Vector4.zero && canopyConnector != null)
            {
              
                
                Vector4 movementVectorAdjusted = new Vector4(movementInputs.x, movementInputs.y, movementInputs.z, movementInputs.w);

                leftSideSurface.SetFlapAngle(movementInputs.y*-1);
                //SetZPosLeft(movementInputs.y);
                //leftSideSurface.Config.chord = chordOriginal+movementInputs.y*riserSensitivity;
                //canopyRb.AddForceAtPosition(canopyRb.transform.up * movementInputs.y*50, rightSideSurface.transform.position);


                rightSideSurface.SetFlapAngle(movementInputs.z*-1);
                //SetZPosRight(movementInputs.z);
                //rightSideSurface.Config.chord = chordOriginal + movementInputs.z * riserSensitivity;
                //canopyRb.AddForceAtPosition(canopyRb.transform.up * movementInputs.z*50, leftSideSurface.transform.position);

                if(movementInputs.y > .6f && movementInputs.z > .6f)
                {
                    float midflap = Mathf.Max(movementInputs.y, movementInputs.z)-.6f;

                    midSurface.SetFlapAngle(midflap);
                }
                else
                {
                    midSurface.SetFlapAngle(0);
                }

                //if (movementInputs.x != 0 && !brakesReleased)
                //{
                //    brakesReleased = true;
                //}
                //if (brakesReleased)
                //{
                //    midSurface.SetFlapAngle(movementInputs.x);
                //
                //}

                canopyRb.AddRelativeTorque ( Vector3.forward*-800 * movementVectorAdjusted.w);

                //leftSideSurface.transform.localEulerAngles += new Vector3(0,0,movementInputs.y);
                //rightSideSurface.transform.localEulerAngles += new Vector3(0,0,movementInputs.z);

                //rb.AddRelativeTorque(Vector3.up * movementVectorAdjusted.w * turnSpeed);

                //rb.AddRelativeTorque

            }
            if (lastMovemntInputs != movementInputs)
            {
                OnMovement?.Invoke(movementInputs);
                lastMovemntInputs = movementInputs;
            }
        }

    }
}
