using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LegMovementAnimator : MonoBehaviour
{
    [SerializeField]
    Transform leftFootBone, leftFootTarget;

    [SerializeField]
    Transform rightFootBone, rightFootTarget;

    [SerializeField]
    TwoBoneIKConstraint leftConstraint;
    [SerializeField]
    TwoBoneIKConstraint rightConstraint;

    MovementController movementController;


    private void Start()
    {
        movementController = GetComponentInParent<MovementController>();

        movementController.OnMovement += HandleMovement;
    }

    private void HandleMovement(Vector4 movement)
    {
        if (movementController.CurrentOrientation == FreefallOrientation.HeadDown || movementController.CurrentOrientation == FreefallOrientation.HeadUp)
        {
            Vector3 leftMidPos = leftFootBone.position+ movementController.CharacterOffset.up*-.2f;
            Vector3 rightMidPos = rightFootBone.position + movementController.CharacterOffset.up * -.2f; ;

            //leftMidPos.z = movementController.CharacterOffset.position.z;
            //rightMidPos.z = movementController.CharacterOffset.position.z;

            
            //leftMidPos.y -= .2f;
            //rightMidPos.y -= .2f;


            leftFootTarget.position = leftMidPos + movementController.CharacterOffset.forward * (movement.z * .4f - movement.x * .1f - movement.w * .1f);// + movementController.transform.right * movement.x * .3f;


            rightFootTarget.position = rightMidPos + movementController.CharacterOffset.forward * (movement.z * .4f + movement.x * .1f + movement.w * .1f);


        }
        else
        {
            Vector3 leftMidPos = leftFootBone.position + movementController.CharacterOffset.up * -.15f;
            Vector3 rightMidPos = rightFootBone.position + movementController.CharacterOffset.up * -.15f; ;

            if(movementController.CurrentOrientation == FreefallOrientation.Back)
            {

                leftFootTarget.position = leftMidPos + movementController.CharacterOffset.forward * (movement.z * -.2f + movement.x * .1f - movement.w * .1f);// + movementController.transform.right * movement.x * .3f;


                rightFootTarget.position = rightMidPos + movementController.CharacterOffset.forward * (movement.z * -.2f - movement.x * .1f + movement.w * .1f);

            }
            if (movementController.CurrentOrientation == FreefallOrientation.Belly)
            {

                leftFootTarget.position = leftMidPos + movementController.CharacterOffset.forward * (movement.z * -.2f - movement.x * .1f - movement.w * .1f);// + movementController.transform.right * movement.x * .3f;


                rightFootTarget.position = rightMidPos + movementController.CharacterOffset.forward * (movement.z * -.2f + movement.x * .1f + movement.w * .1f);

            }


        }


        rightConstraint.weight = Mathf.Clamp(Mathf.Clamp(movement.x,0,1)*.3f+ Mathf.Abs(movement.z * .6f),0,.85f);
        leftConstraint.weight = Mathf.Clamp(Mathf.Clamp(movement.x,-1,0)*-.3f+ Mathf.Abs(movement.z * .6f), 0, .85f);

        //transform.rotation = footBone.transform.rotation;
    }
}
