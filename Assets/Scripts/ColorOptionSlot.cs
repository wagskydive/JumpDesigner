using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorOptionSlot : MonoBehaviour
{
    public event Action<Color> OnColorChange;
    public event Action<int> OnSelect;

    public int index;

    [SerializeField]
    Image colorPanel;
    [SerializeField]
    TMP_Text textField;

    ColorOptions colorOptions;

    private void Start()
    {
        colorOptions = GetComponentInParent<ColorOptions>();

    }
    public void SelectColorOption()
    {
        OnSelect?.Invoke(index);
    }


    public void SetColor(Color color)
    {
        colorPanel.color = color;
    }

    public void SetText(string text)
    {
        textField.text = text;
    }

    public void AttachColorPicker()
    {
        ColorPicker.OnColorChanged += HandleColorChange;
    }

    public void DetachColorPicker()
    {
        ColorPicker.OnColorChanged -= HandleColorChange;
    }



    private void HandleColorChange(Color color)
    {
        SetColor(color);
        OnColorChange?.Invoke(color);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
