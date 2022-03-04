using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler2D : MonoBehaviour
{
    Selectable2D Selected;
    public static event Action<int, FreefallOrientation> OnOrientationChange;
    public static event Action<int, float> OnRotationChange;

    public static event Action<int, int, int> OnMoveToRequest;

    private void Start()
    {
        OrientationButton.OnButtonPress += OrientationButtonPressed;
        RotationButton.OnButtonPress += RotationButtonPressed;
        Deselector2D.OnDeselectAny += DeselectAny;
        Selectable2D.OnSelected += Select;
        Selectable2D.OnSlotClicked += SlotClicked;


    }

    private void SlotClicked(int index, int slot)
    {

        if(Selected != null && Selected.Index != index && Selected.Index != 0)
        {
            OnMoveToRequest?.Invoke(Selected.Index, index, slot);
        }
    }

    private void Select(Selectable2D obj)
    {
        Selected = obj;
    }

    private void DeselectAny()
    {
        Selected = null;
    }


    private void OrientationButtonPressed(FreefallOrientation orientation)
    {
        if(Selected != null)
        {
            OnOrientationChange?.Invoke(Selected.Index,orientation);
        }
    }

    private void RotationButtonPressed(float rotation)
    {
        if(Selected != null)
        {
            OnRotationChange?.Invoke(Selected.Index, rotation);
        }
    }
}
