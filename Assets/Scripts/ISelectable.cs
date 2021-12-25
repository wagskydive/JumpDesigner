using UnityEngine;
using System;

public interface ISelectable
{

    event Action OnSelected; 
    event Action OnDeselected; 
    Transform transform { get; }

    void Select();
    void Deselect();
}
