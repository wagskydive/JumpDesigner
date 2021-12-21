using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkHint : MonoBehaviour
{
    [SerializeField]
    Transform followOnIntactive;

    [SerializeField]
    Hand hand;

    public bool Activated;

    private void Start()
    {
        hand.OnDock += Activate;
        hand.OnUnDock += DeActivate;
    }

    private void Activate()
    {
        Activated = true;
    }

    private void DeActivate()
    {
        Activated = false;
    }


    private void FixedUpdate()
    {
        if (!Activated)
        {
            transform.position = followOnIntactive.position;
        }
    }
}
