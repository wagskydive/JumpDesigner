using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public enum FreefallOrientation
{
    HeadDown,
    Back,
    HeadUp,
    Belly


}

public class OrientationHandler
{
    public void Transition(FreefallOrientation currentOrientation, FreefallOrientation endOrientation, GameObject CharacterOffset, float transitionSpeed)
    {


        int difference = (int)endOrientation - (int)currentOrientation;

        if (difference == 3)
        {
            difference -= 4;
            if (difference == 13)
            {
                difference += 4;


                float rotation = difference * 90;


                LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, rotation, transitionSpeed * Mathf.Abs(difference));
            }
        }
    }


    //void HandleHeadDown()
    //{
    //
    //    CharacterOffset.transform.localEulerAngles = new Vector3(180, 0, 0);
    //}
    //
    //
    //void HandleBack()
    //{
    //
    //    CharacterOffset.transform.localEulerAngles = new Vector3(-90, 0, 0);
    //}
    //
    //
    //void HandleHeadUp()
    //{
    //    CharacterOffset.transform.localEulerAngles = new Vector3(0, 0, 0);
    //}
    //
    //
    //void HandleBelly()
    //{
    //    CharacterOffset.transform.localEulerAngles = new Vector3(90, 0, 0);
    //}
    //
    //
    //
    //
    //void Transition(FreefallOrientation end)
    //{
    //    if (CurrentOrientation != end)
    //    {
    //
    //
    //        //transitionTimer = transitionSpeed;
    //        //Vector3 axisWorld = transform.TransformDirection(axis);
    //        if (end == FreefallOrientation.HeadDown)
    //        {
    //            if (CharacterOffset.transform.localEulerAngles == new Vector3(-90, 0, 0))
    //            {
    //                CharacterOffset.transform.localEulerAngles += Vector3.right * 360;
    //            }
    //            LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, 180 - CharacterOffset.transform.localEulerAngles.x, transitionSpeed).setOnComplete(HandleHeadDown);
    //            //LeanTween.rotateLocal(CharacterOffset.transform.gameObject, Vector3.right*180, transitionSpeed);
    //        }
    //        if (end == FreefallOrientation.Back)
    //        {
    //            if (CharacterOffset.transform.localEulerAngles == new Vector3(180, 0, 0))
    //            {
    //                CharacterOffset.transform.localEulerAngles -= Vector3.right * 360;
    //            }
    //            LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, -90 - CharacterOffset.transform.localEulerAngles.x, transitionSpeed).setOnComplete(HandleBack);
    //        }
    //        if (end == FreefallOrientation.HeadUp)
    //        {
    //            LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, -CharacterOffset.transform.localEulerAngles.x, transitionSpeed).setOnComplete(HandleHeadUp);
    //        }
    //        if (end == FreefallOrientation.Belly)
    //        {
    //            LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, 90 - CharacterOffset.transform.localEulerAngles.x, transitionSpeed).setOnComplete(HandleBelly);
    //        }
    //        //LeanTween.rotateAroundLocal(CharacterOffset.gameObject, CharacterOffset.transform.InverseTransformDirection(axisWorld), 90 * repeat, transitionSpeed);//.followDamp(dude1, followArrow, LeanProp.scale, 1.1f);
    //        //CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 90 * repeat);
    //        CurrentOrientation = end;
    //
    //
    //
    //        SetColliderAxis(end);
    //
    //        OnTransition?.Invoke(end);
    //    }
    //
    //}
}

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class MovementController : MonoBehaviour
{
    public event Action<FreefallOrientation, float> OnTransition;
    public event Action<Vector4> OnMovement;

    Rigidbody rb;

    [SerializeField]
    public Transform CharacterOffset;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed = 4f;
    [SerializeField]
    public float xOffset = 0;

    CapsuleCollider capsCollider;

    //[SerializeField]
    //SkinnedMeshRenderer meshRenderer;

    //[SerializeField]
    //Transform pelvis;


    [SerializeField]
    public IInput inputSource;

    Gyroscope gyro;

    public bool TransitionPossible
    {
        get => transitionTimer == 0;
    }
    [SerializeField]
    private float transitionSpeed = 2;

    public float TransitionSpeed { get => transitionSpeed; }


    bool directControl;

    float transitionTimer = 0;

    public FreefallOrientation CurrentOrientation = FreefallOrientation.Belly;

    public int controlMode { get; private set; }


    public void Transition(FreefallOrientation end)
    {


        int difference = (int)end - (int)CurrentOrientation;

        if (difference == 3)
        {
            difference -= 4;
        }
        if (difference == -3)
        {
            difference += 4;
        }

        float rotation = difference * 90;


        //LeanTween.rotateAroundLocal(CharacterOffset.transform.gameObject, Vector3.right, rotation, transitionSpeed * Mathf.Abs(difference));


        CurrentOrientation = end;



        SetColliderAxis(end);

        OnTransition?.Invoke(end,rotation);
        OnMovement?.Invoke(inputSource.MovementVector);
    }





    public void TransitionForward()
    {
        int nextOrentationIndex = (int)CurrentOrientation + controlMode;
        if (nextOrentationIndex > 3) { nextOrentationIndex = 0; }
        Transition((FreefallOrientation)nextOrentationIndex);
    }


    public void TransitionBackward()
    {
        int nextOrentationIndex = (int)CurrentOrientation - controlMode;
        if (nextOrentationIndex < 0) { nextOrentationIndex = 3; }
        Transition((FreefallOrientation)nextOrentationIndex);
    }


    public void TransitionRight()
    {
        int nextOrentationIndex = (int)CurrentOrientation + 2;
        if (nextOrentationIndex > 3) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex);
        controlMode *= -1;
    }


    public void TransitionLeft()
    {
        int nextOrentationIndex = (int)CurrentOrientation + 2;
        if (nextOrentationIndex > 3) { nextOrentationIndex -= 4; }
        Transition((FreefallOrientation)nextOrentationIndex);
        controlMode *= -1;
    }

    public void Turn180Left()
    {
        //Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        //CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), 180);
        //Transition(CurrentOrientation);
        controlMode *= -1;

    }

    public void Turn180Right()
    {
        //Vector3 axisWorld = transform.TransformDirection(Vector3.up);
        //CharacterOffset.transform.Rotate(CharacterOffset.transform.InverseTransformDirection(axisWorld), -180);


        //Transition(CurrentOrientation);
        controlMode *= -1;
    }


    void HandleTransitionTimer()
    {
        if (transitionTimer > 0)
        {
            transitionTimer -= Time.fixedDeltaTime;
        }

        if (transitionTimer < 0)
        {
            transitionTimer = 0;
        }
    }

    public void ReplaceInput(IInput newInput)
    {
        if (inputSource != null)
        {
            inputSource.OnButtonPressed -= HandleButtonPress;
        }

        inputSource = newInput;

        if (newInput != null)
        {
            inputSource.OnButtonPressed += HandleButtonPress;
        }

    }





    private void HandleButtonPress(int obj)
    {
        if (obj == 0)
        {
            Transition(FreefallOrientation.HeadDown);
        }
        if (obj == 1)
        {
            Transition(FreefallOrientation.Back);
        }
        if (obj == 2)
        {
            Transition(FreefallOrientation.HeadUp);
        }
        if (obj == 3)
        {
            Transition(FreefallOrientation.Belly);
        }

        if (obj == 4)
        {
            TransitionForward();
        }
        if (obj == 5)
        {
            TransitionBackward();
        }
        if (obj == 6)
        {
            TransitionLeft();
        }
        if (obj == 7)
        {
            TransitionRight();
        }
        if (obj == 8)
        {
            Turn180Left();
        }
        if (obj == 9)
        {
            Turn180Right();
        }



        if (obj == 11)
        {
            LeanTransitionForward();
        }
        if (obj == 12)
        {
            LeanTransitionBackward();
        }
    }

    private void LeanTransitionForward()
    {
        if(CurrentOrientation == FreefallOrientation.Back || CurrentOrientation == FreefallOrientation.Belly)
        {
            TransitionForward();
        }
        else { TransitionBackward(); }
    }

    private void LeanTransitionBackward()
    {
        if (CurrentOrientation == FreefallOrientation.Back || CurrentOrientation == FreefallOrientation.Belly)
        {
            TransitionBackward();
        }
        else { TransitionForward(); }
    }

    void SetColliderAxis(FreefallOrientation orientation)
    {
        if (orientation == FreefallOrientation.Back || orientation == FreefallOrientation.Belly)
        {
            capsCollider.direction = 2;

        }
        else
        {
            capsCollider.direction = 1;
        }
        //collider.center = transform.InverseTransformPoint(meshRenderer.bounds.center);// transform.InverseTransformPoint(pelvis.position);
    }



    public event Action OnPull;

    private void Awake()
    {
        capsCollider = GetComponent<CapsuleCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        controlMode = 1;
    }


    public void PullParachute()
    {
        CanopyController canopyController =  gameObject.AddComponent<CanopyController>();
        gameObject.AddComponent<CanopyManualControlHelper>();
        NPC_Ai_FromState nPC_Ai_FromState = GetComponent<NPC_Ai_FromState>();
        if (nPC_Ai_FromState != null)
        {
            nPC_Ai_FromState.enabled = false;
        }
        canopyController.Pull();
        rb.freezeRotation = false;
        Invoke("ResetOffset", 1.4f);
        OnPull?.Invoke();
        enabled = false;
    }

    void ResetOffset()
    {
        CharacterOffset.localEulerAngles = Vector3.zero;
    }

    float ForwardSign(FreefallOrientation orientation)
    {
        int cur = 1;
        if (orientation == FreefallOrientation.HeadUp || orientation == FreefallOrientation.HeadDown)
        {
            cur = -1;
        }
        return cur * controlMode;

    }

    Vector4 lastMovemntInputs = Vector4.zero;

    private void FixedUpdate()
    {

        //HandleTransitionTimer();

        if (inputSource == null)
        {
            if (GetComponent<IInput>() != null)
            {
                ReplaceInput(GetComponent<IInput>());
            }

        }

        if (inputSource != null)
        {
            Vector4 movementInputs = inputSource.MovementVector;
            if (movementInputs != Vector4.zero)
            {

                if (Convert.ToString(inputSource.CurrentButtonsState, 2).EndsWith("1"))
                {

                }
                else
                {
                    Vector4 movementVectorAdjusted = new Vector4(movementInputs.x, movementInputs.y, movementInputs.z, movementInputs.w);

                    rb.AddRelativeForce(movementVectorAdjusted * movementSpeed);


                    rb.AddTorque(Vector3.up * movementVectorAdjusted.w * turnSpeed);

                }
                
            }
            if(lastMovemntInputs != movementInputs)
            {
                OnMovement?.Invoke(movementInputs);
                lastMovemntInputs = movementInputs;
            }
        }

        rb.AddForce(new Vector3(0, -SpeedMultiplierFromOrientation(CurrentOrientation), 0) * movementSpeed);
        rb.AddForce(new Vector3(0, -20, 0));
    }



    float SpeedMultiplierFromOrientation(FreefallOrientation orientation)
    {
        if (orientation == FreefallOrientation.Belly)
        {
            return 0;
        }
        if (orientation == FreefallOrientation.Back)
        {
            return 0.2f;
        }
        if (orientation == FreefallOrientation.HeadUp)
        {
            return 0.7f;
        }
        else
        {
            return 0.9f;
        }
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    public void SetOffset(float offset)
    {
        xOffset = offset;
    }

}
