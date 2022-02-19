using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOptions : MonoBehaviour
{
    [SerializeField]
    ColorHelper CurrentColorObject;

    [SerializeField]
    ColorOptionSlot ColorOption1;
    [SerializeField]
    ColorOptionSlot ColorOption2;
    [SerializeField]
    ColorOptionSlot ColorOption3;
    [SerializeField]
    ColorOptionSlot ColorOption4;

    int currentSelection = 0;

    [SerializeField]
    ColorSelector colorSelector;

    private void Start()
    {
        RedrawUi();
        ColorOption1.OnColorChange += ChangeColor1;
        ColorOption2.OnColorChange += ChangeColor2;
        ColorOption3.OnColorChange += ChangeColor3;
        ColorOption4.OnColorChange += ChangeColor4;
        colorSelector.OnSelectionChanged += HandleSelectionChange;
    }

    void SetCurrentColorObject(ColorHelper colorHelper)
    {
        CurrentColorObject = colorHelper;

    }

    void HandleSelectionChange(int newSelection)
    {
        if(currentSelection == 0)
        {
            ColorOption1.DetachColorPicker();
        }
        if (currentSelection == 1)
        {
            ColorOption2.DetachColorPicker();
        }
        if (currentSelection == 2)
        {
            ColorOption3.DetachColorPicker();
        }
        if (currentSelection == 3)
        {
            ColorOption4.DetachColorPicker();
        }


        if (newSelection == 0)
        {
            ColorOption1.ConnectColorPicker();
        }
        if (newSelection == 1)
        {
            ColorOption2.ConnectColorPicker();
        }
        if (newSelection == 2)
        {
            ColorOption3.ConnectColorPicker();
        }
        if (newSelection == 3)
        {
            ColorOption4.ConnectColorPicker();
        }
        currentSelection = newSelection;
    }

    void RedrawUi()
    {

        if(CurrentColorObject.Material_1 != null)
        {
            ColorOption1.gameObject.SetActive(true);
            ColorOption1.SetColor(CurrentColorObject.Color_1);
        }
        else
        {
            ColorOption1.gameObject.SetActive(false);
        }
        
        if(CurrentColorObject.Material_2 != null)
        {
            ColorOption2.gameObject.SetActive(true);
            ColorOption2.SetColor(CurrentColorObject.Color_2);
        }
        else
        {
            ColorOption2.gameObject.SetActive(false);
        }

        if (CurrentColorObject.Material_3 != null)
        {
            ColorOption3.gameObject.SetActive(true);
            ColorOption3.SetColor(CurrentColorObject.Color_3);
        }
        else
        {
            ColorOption3.gameObject.SetActive(false);
        }

        if (CurrentColorObject.Material_4 != null)
        {
            ColorOption4.gameObject.SetActive(true);
            ColorOption4.SetColor(CurrentColorObject.Color_4);
        }
        else
        {
            ColorOption4.gameObject.SetActive(false);
        }

    }

    void ChangeColor1(Color color)
    {
        CurrentColorObject.Color_1 = color;
        CurrentColorObject.Redraw();
    }


    void ChangeColor2(Color color)
    {
        CurrentColorObject.Color_2 = color;
        CurrentColorObject.Redraw();
    }

    void ChangeColor3(Color color)
    {
        CurrentColorObject.Color_3 = color;
        CurrentColorObject.Redraw();
    }

    void ChangeColor4(Color color)
    {
        CurrentColorObject.Color_4 = color;
        CurrentColorObject.Redraw();
    }


}
