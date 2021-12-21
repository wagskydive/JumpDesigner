using UnityEngine;

public interface IInput
{
    Vector4 MovementVector { get; }
    int CurrentButtonsState { get; }

    

}
