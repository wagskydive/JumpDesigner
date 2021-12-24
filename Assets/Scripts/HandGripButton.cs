using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HandGripButton : MonoBehaviour
{
    [SerializeField]
    bool isLeft;

    [SerializeField]
    Image image;


    Hand hand;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        SelectionHandler.OnSelectionConfirmed += HandleSelection;
    }

    private void HandleSelection(ISelectable obj)
    {
        if(obj.transform.GetComponent<Gripper>() != null)
        {
            AssignGripper(obj.transform.GetComponent<Gripper>());
        }
    }

    Gripper _gripper;

    public void AssignGripper(Gripper gripper)
    {
        _gripper = gripper;
        if(hand != null)
        {
            hand.OnGripSense-= SetGripFound;
            hand.OnGripUnSense -= SetGripUnFound;
        }
        if (isLeft)
        {
            hand = gripper.leftHand;
            hand.OnGripSense += SetGripFound;
            
        }
        else
        {
            hand = gripper.rightHand;
            hand.OnGripUnSense += SetGripUnFound;
        }
    }

    private void SetGripUnFound()
    {
        gripFound = false;
        image.enabled = false;
    }

    private void SetGripFound()
    {
        gripFound = true;
        image.color = Color.green;
        image.enabled = true;
    }

    bool gripFound;
    bool docked;

    public void Click()
    {
        if (gripFound && !docked)
        {
            Dock();
        }
        else if (docked)
        {
            UnDock();           
        }
        
        
    }

    void Dock()
    {
        if (isLeft)
        {
            _gripper.LeftDock();
        }
        else
        {
            _gripper.RightDock();
        }
        docked = true;
        image.color = Color.red;
    }

    public void UnDock()
    {
        if (isLeft)
        {
            _gripper.LeftUnDock();
        }
        else
        {
            _gripper.RightUnDock();
        }
        docked = false;
        image.color = Color.green;
    }
}