using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Selectable : MonoBehaviour, ISelectable
{
    public event Action OnSelected;
    public event Action OnDeselected;

    public void Select()
    {       
        OnSelected?.Invoke();
    }


    public void Deselect()
    {        
        OnDeselected?.Invoke();
    }
}