using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorOptions : MonoBehaviour
{
    [SerializeField]
    ColorHelper CurrentColorObject;

    [SerializeField]
    GameObject ColorOptionSlotPrefab;

    [SerializeField]
    ColorOptionSlot[] ColorOptionSlots;


    int currentSelection = 0;

    [SerializeField]
    ColorSelector colorSelector;

    private void Start()
    {       
        colorSelector.OnSelectionChanged += HandleSelectionChange;
        if(CurrentColorObject == null)
        {
            SetCurrentColorObject(FindObjectOfType<ColorHelper>());
        }
    }

    public void SetCurrentColorObject(ColorHelper colorHelper)
    {
        CurrentColorObject = colorHelper;
        if (ColorOptionSlots.Any())
        {
            for (int i = 0; i < ColorOptionSlots.Length; i++)
            {
                Destroy(ColorOptionSlots[i].gameObject);
            }
        }

        ColorOptionSlots = new ColorOptionSlot[CurrentColorObject.materialsToAffect.Length];

        for (int i = 0; i < CurrentColorObject.materialsToAffect.Length; i++)
        {
            ColorOptionSlot colorOptionSlot = Instantiate(ColorOptionSlotPrefab, transform).GetComponent<ColorOptionSlot>();
            colorOptionSlot.SetText(CurrentColorObject.materialsToAffect[i].name);
            colorOptionSlot.index = i;
            colorOptionSlot.OnSelect += HandleSelectionChange;
            colorOptionSlot.OnColorChange += ChangeColor;
            ColorOptionSlots[i] = colorOptionSlot;
        }
        RedrawUi();
    }

    void HandleSelectionChange(int newSelection)
    {
        colorSelector.SetSelection(newSelection);
        if(currentSelection != -1)
        {
            ColorOptionSlots[currentSelection].DetachColorPicker();
        }

        ColorOptionSlots[newSelection].AttachColorPicker();

        currentSelection = newSelection;
    }

    void RedrawUi()
    {
        for (int i = 0; i < ColorOptionSlots.Length; i++)
        {
            ColorOptionSlots[i].SetColor(CurrentColorObject.colors[i]);
        }

    }


    void ChangeColor(Color color)
    {
        CurrentColorObject.ChangeColor(currentSelection,color);
        CurrentColorObject.RedrawMaterialsInScene();
    }


}
