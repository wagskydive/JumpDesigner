using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{


    [SerializeField]
    public Material[] materialsToAffect;

    public Color[] colors;

    public void Initialize()
    {
        colors = new Color[materialsToAffect.Length];

        for (int i = 0; i < materialsToAffect.Length; i++)
        {
            colors[i] = materialsToAffect[i].color;
        }
    }


    public void RedrawMaterialsInScene()
    {
        for (int i = 0; i < materialsToAffect.Length; i++)
        {
            materialsToAffect[i].color = colors[i];
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void ChangeColor(int index, Color color)
    {

        colors[index] = color;
        RedrawMaterialsInScene();
    }
}
