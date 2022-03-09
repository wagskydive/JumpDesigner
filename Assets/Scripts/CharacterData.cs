using System.Collections.Generic;
using UnityEngine;

public class CharacterData : ScriptableObject
{
    string name;

    Color[] colors;

    public Color[] Colors { get => colors; }

    public string Name { get => name; }

    List<OwnedItem> inventory;

    public List<OwnedItem> Inventory { get => inventory; }

    public CharacterData(string _name)
    {
        name = _name;
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
