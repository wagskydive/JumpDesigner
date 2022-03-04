using System;
using UnityEngine;
using UnityEngine.UI;

public class Selected2DColorHandler : MonoBehaviour
{
    Image image;

    Selectable2D selectable2d;


    Color originalColor;
    private void Start()
    {
        image = GetComponent<Image>();
        
        selectable2d = GetComponent<Selectable2D>();
        Selectable2D.OnSelected += HandleSelected;
        selectable2d.OnDeselected += HandleDeselect;
    }

    private void OnDestroy()
    {
        Selectable2D.OnSelected -= HandleSelected;
        selectable2d.OnDeselected -= HandleDeselect;
    }

    private void HandleDeselect()
    {
        image.color = originalColor;
    }

    private void HandleSelected(Selectable2D obj)
    {
        if(obj == selectable2d)
        {
            originalColor = image.color;
            image.color = Color.green;
        }
    }
}
