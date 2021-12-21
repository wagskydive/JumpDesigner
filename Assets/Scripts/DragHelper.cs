using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHelper : MonoBehaviour, IEndDragHandler
{
    public event Action ÓnDragEndEvent;

    public void OnEndDrag(PointerEventData eventData)
    {
        ÓnDragEndEvent?.Invoke();
    }

}