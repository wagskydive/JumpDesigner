using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gripper : MonoBehaviour
{
    [SerializeField]
    public Hand leftHand;

    [SerializeField]
    public Hand rightHand;

    bool leftGripFound;
    bool leftDocked;
    bool rightGripFound;
    bool rightDocked;

    private void Awake()
    {
        Grip.OnSense += GripSensed;
        Grip.OnUnSense += GripUnSensed;
    }



    private void GripUnSensed(Grip grip, Hand handSensed)
    {
        if (handSensed == leftHand)
        {
            leftGripFound = false;
            //leftHand.GripUnSense(grip);
        }
        if (handSensed == rightHand)
        {
            rightGripFound = false;
            //rightHand.GripUnSense(grip);
        }
    }

    private void GripSensed(Grip grip, Hand handSensed)
    {
        if (handSensed == leftHand)
        {
            leftGripFound = true;
            //leftHand.GripSense(grip);
        }
        if (handSensed == rightHand)
        {
            rightGripFound = true;
            //rightHand.GripSense(grip);
        }
    }



    public void LeftDock()
    {
        leftDocked = true;
        leftHand.DockCommand();

    }

    public void RightDock()
    {
        rightDocked = true;
        rightHand.DockCommand();

    }

    public void LeftUnDock()
    {
        leftDocked = false;
        leftHand.UnDockCommand();

    }


    public void RightUnDock()
    {
        rightDocked = false;
        rightHand.UnDockCommand();

    }



}
