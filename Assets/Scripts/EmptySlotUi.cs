using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptySlotUi : MonoBehaviour,IPointerClickHandler
{
    public event Action<int> OnSlotClick;

    [SerializeField]
    int slotIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSlotClick?.Invoke(slotIndex);
    }
}
