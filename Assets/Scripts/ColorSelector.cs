using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    public event Action<int> OnSelectionChanged;

    int currentSelection = -1;

    [SerializeField]
    Transform arrow;

    private void Start()
    {
        arrow.gameObject.SetActive(false);
    }

    public void SetSelection(int selection)
    {
        if(currentSelection != selection)
        {
            arrow.gameObject.SetActive(true);
            currentSelection = selection;
            arrow.localPosition = Vector3.up * selection * -56;
            OnSelectionChanged?.Invoke(currentSelection);
        }

    }
}
