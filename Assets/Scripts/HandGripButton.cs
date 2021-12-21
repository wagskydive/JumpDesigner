﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HandGripButton : MonoBehaviour,IPointerClickHandler
{

    [SerializeField]
    Button button;

    [SerializeField]
    Hand hand;

    private void Awake()
    {
        button = GetComponent<Button>();
        Grip.OnSense += GripSensed;
        Grip.OnUnSense += GripUnSensed;
        button.image.enabled = false;
    }

    bool gripFound;
    bool docked;

    private void GripUnSensed(Grip grip, Hand handSensed)
    {
        if (handSensed == hand)
        {
            button.image.enabled = false;
            gripFound = false;

        }
    }

    private void GripSensed(Grip grip, Hand handSensed)
    {
        if(handSensed == hand)
        {
            gripFound = true;
            button.image.enabled = true;
            button.image.color = Color.green;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gripFound)
        {
            docked = true;
            hand.GripCommand();
            button.image.color = Color.red;
            hand.OnUnDock += UnDock;
        }
        if (docked)
        {
            hand.GripCommand();
        }
        
        
    }

    public void UnDock()
    {
        docked = false;
        button.image.color = Color.green;
        hand.OnUnDock -= UnDock;
    }
}