using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyConnector : MonoBehaviour
{
    [SerializeField]
    SpringJoint[] joints;
    [SerializeField]
    AeroSurface leftControl;
    [SerializeField]
    AeroSurface rightControl;
    [SerializeField]
    AeroSurface midControl;


    public void ConnectCanopy(CanopyController bodyToConnect)
    {
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].connectedBody = bodyToConnect.GetComponent<Rigidbody>();
        }
        bodyToConnect.SetControls(leftControl, rightControl, midControl);
    }

    public void CutawayCanopy()
    {
        for (int i = 0; i < joints.Length; i++)
        {
            joints[i].connectedBody = null;
        }
    }
}
