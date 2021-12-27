using System;
using UnityEngine;


public interface IInput
{
    event Action<int> OnButtonPressed;

    Vector4 MovementVector { get; }
    int CurrentButtonsState { get; }

}
