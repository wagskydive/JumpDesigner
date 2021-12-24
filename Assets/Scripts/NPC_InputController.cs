using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_InputController : MonoBehaviour, IInput
{
    public Vector4 MovementVector => AiMovement();

    public event Action<int> OnButtonPressed;

    private Vector4 AiMovement()
    {
        return Vector4.zero;
    }

    public int CurrentButtonsState => AiButtons();

    private int AiButtons()
    {
        int buttonState = 0;
        if (SelectionHandler.Instance.HasSelection())
        {
            buttonState += 1;
        }

        return buttonState;
    }

}
