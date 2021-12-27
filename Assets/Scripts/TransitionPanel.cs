using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransitionPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action OnTransitionPanelEnter;
    public event Action OnTransitionPanelExit;
        
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter TransitionPanel");
        OnTransitionPanelEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit TransitionPanel");
        OnTransitionPanelExit?.Invoke();
    }
}
