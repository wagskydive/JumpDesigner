using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{
    public Color Color_1;
    public Color Color_2;
    public Color Color_3;
    public Color Color_4;

    public Material Material_1;
    public Material Material_2;
    public Material Material_3;
    public Material Material_4;



    public void Redraw()
    {
        if(Material_1 != null)
        {
            Material_1.color = Color_1;
        }
        if(Material_2 != null)
        {
            Material_2.color = Color_2;
        }
        if(Material_3 != null)
        {
            Material_3.color = Color_3;
        }
        if(Material_4 != null)
        {
            Material_4.color = Color_4;
        }


    }
    private void Start()
    {
        Redraw();
    }

    public void ChangeColor(int index, Color color)
    {
        if(index == 0)
        {
            Color_1 = color;
        }
        if (index == 1)
        {
            Color_2 = color;
        }
        if (index == 2)
        {
            Color_3 = color;
        }

        if (index == 3)
        {
            Color_4 = color;
        }
    }
}
