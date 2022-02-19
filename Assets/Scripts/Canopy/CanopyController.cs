using System;
using System.Collections.Generic;
using UnityEngine;



public class CanopyController : MonoBehaviour
{
    public event Action<Vector4> OnMovement;
    public event Action OnPull;
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

    Rigidbody canopyRb;

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
        midSurface.SetFlapAngle(.2f);
        LeanTween.scale(go, Vector3.one, 2);


        brakesReleased = false;
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

                leftSideSurface.SetFlapAngle(movementInputs.y);
                rightSideSurface.SetFlapAngle(movementInputs.z);

                if(movementInputs.x != 0 && !brakesReleased)
                {
                    brakesReleased = true;
                }
                if (brakesReleased)
                {
                    midSurface.SetFlapAngle(movementInputs.x);

                }

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
