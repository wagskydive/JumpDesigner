using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorHelper : MonoBehaviour
{
    
    public List<Material> materialsToAffect;

    public Color[] colors;

    public void LoadColorsFromCharacterData(CharacterData data)
    {
        colors = data.Colors;

    }

    

    public void Initialize()
    {
        colors = new Color[materialsToAffect.Count];

        for (int i = 0; i < materialsToAffect.Count; i++)
        {
            colors[i] = materialsToAffect[i].color;
        }
    }
    public void GetMaterialsToAffect()
    
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        materialsToAffect = new List<Material>();
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] mats = renderers[i].materials;
            for (int j = 0; j < mats.Length; j++)
            {
                materialsToAffect.Add(mats[j]);
            }           
        }
        
    }

    public void RedrawMaterialsInScene()
    {
        for (int i = 0; i < materialsToAffect.Count; i++)
        {
            materialsToAffect[i].color = colors[i];
        }
    }

    private void Start()
    {
        GetMaterialsToAffect();
        Initialize();
    }

    public void ChangeColor(int index, Color color)
    {

        colors[index] = color;
        RedrawMaterialsInScene();
    }
}
