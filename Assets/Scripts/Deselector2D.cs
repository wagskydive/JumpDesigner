using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deselector2D : MonoBehaviour, IPointerClickHandler
{
    public static event Action OnDeselectAny;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDeselectAny?.Invoke();
    }
}
