using System;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public static event Action<float> OnButtonPress;

    [SerializeField]
    float rotateAmount;

    public void ButtonPress()
    {
        OnButtonPress?.Invoke(rotateAmount);
    }
}
