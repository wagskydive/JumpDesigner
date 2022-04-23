using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    string name;

    string gender;

    public string Gender {get => gender;}

    string occupation;

    public string Occupation { get => occupation; }

    Color[] colors;

    float talent;

    public float Talent{ get => talent;}

    public Color[] Colors { get => colors; }

    public string Name { get => name; }

    List<OwnedItem> inventory;

    public List<OwnedItem> Inventory { get => inventory; }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Art/Icons/man-person") as Sprite;
    }

    public Logbook logbook;

    public CharacterData(string _name, Color[] _colors = null, List<OwnedItem> _inventory = null, Logbook _logbook = null,string _gender = null, string _occupation = null, float _talent = 1)
    {
        name = _name;
        colors = _colors;
        inventory = _inventory;
        logbook = _logbook;
        gender = _gender;
        occupation = _occupation;
        talent = _talent;

        if (colors == null)
        {
            colors = new Color[3];
            colors[0] = Color.red;
            colors[1] = Color.green;
            colors[2] = Color.blue;
        }
        if (inventory == null)
        {
            inventory = new List<OwnedItem>();
        }
        if (logbook == null)
        {
            logbook = new Logbook();
        }

    }

    public void SetColors(Color[] _colors)
    {
        colors = _colors;
    }

    public void SetInventory(List<OwnedItem> _inventory)
    {
        inventory = _inventory;
    }

    public void Rename(string newName)
    {
        name = newName;
    }
}
