using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationButton : MonoBehaviour
{
    public static event Action<FreefallOrientation> OnButtonPress;

    [SerializeField]
    FreefallOrientation orientation;

    public void ButtonPress()
    {
        OnButtonPress?.Invoke(orientation);
    }
}
