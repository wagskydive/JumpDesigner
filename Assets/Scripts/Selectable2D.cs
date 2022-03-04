using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable2D : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Selectable2D> OnSelected;
    public event Action OnDeselected;

    public static event Action<int, int> OnSlotClicked;

    bool isSelected;
    [SerializeField]
    EmptySlotUi[] emptySlotUis;

    public int Index { get; private set; }

    private void Awake()
    {
        Deselector2D.OnDeselectAny += ExternalDeselect;
        for (int i = 0; i < emptySlotUis.Length; i++)
        {
            emptySlotUis[i].OnSlotClick += SlotClicked;
        }

    }

    private void SlotClicked(int slot)
    {
        OnSlotClicked?.Invoke(Index,slot);
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    private void ExternalDeselect()
    {
        if (isSelected)
        {
            isSelected = false;
            Selectable2D.OnSelected -= Deselect;
            OnDeselected?.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            isSelected = true;

            OnSelected?.Invoke(this);
            Selectable2D.OnSelected += Deselect;
        }

    }

    private void Deselect(Selectable2D selectable)
    {
        if(isSelected && selectable != this)
        {
            isSelected = false;
            Selectable2D.OnSelected -= Deselect;
            OnDeselected?.Invoke();
        }
    }
}
