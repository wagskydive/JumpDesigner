using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class Hand : MonoBehaviour
{
    public event Action OnGripSense;
    public event Action OnGripUnSense;

    public event Action OnUnDock;
    public event Action OnDock;

    SphereCollider sphereCollider;

    Grip availableGrip = null;

    [SerializeField]
    Transform handBone;

    [SerializeField]
    TwoBoneIKConstraint constraint;

    public void GripSense(Grip sensorGrip)
    {
        availableGrip = sensorGrip;
        OnGripSense?.Invoke();
    }
    
    public void GripUnSense(Grip senserGrip)
    {
        availableGrip = null;
        dockedTo = null;
        OnGripUnSense?.Invoke();
    }


    private void Awake()
    {
        parentBody = GetComponentInParent<Rigidbody>();

        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = .03f;
        sphereCollider.isTrigger = true;

    }

    Rigidbody parentBody;

    Rigidbody dockedTo;

    

    public void ExecuteDock(Grip grip)
    {

        dockedTo = grip.GetComponentInParent<Rigidbody>();
        OnDock?.Invoke();
    }

    public void UnDock()
    {
        dockedTo = null;
        OnUnDock?.Invoke();
    }
    

    private void FixedUpdate()
    {
        if(availableGrip != null && dockedTo != null)
        {
            Vector3 averageVelocity = (dockedTo.velocity + parentBody.velocity) / 2;

            parentBody.velocity = (averageVelocity + parentBody.velocity) / 2;
            dockedTo.velocity = (averageVelocity+ dockedTo.velocity) / 2;

            transform.position = availableGrip.transform.position;
            constraint.weight = 1;
        }
        else
        {
            transform.position = handBone.transform.position;
            transform.rotation = handBone.transform.rotation;
            constraint.weight = 0;
        }
    }

    public void UnDockCommand()
    {
        if (dockedTo != null)
        {
            UnDock();
        }
    }

    public void DockCommand()
    {
        if(availableGrip != null)
        {
            
            ExecuteDock(availableGrip);

        }
    }
}
