using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase
{
    private static ItemDatabase instance;
    public static ItemDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ItemDatabase();
                instance.LoadItems();
            }
            return instance;
        }

    }

    

    private Dictionary<string, OwnableType> items = new Dictionary<string, OwnableType>();

    public Dictionary<string, OwnableType> Items
    {
        get
        {
            return items;
        }
    }

    public OwnableType GetItem(string itemName)
    {
        return items[itemName];
    }

    public void LoadItems()
    {
        OwnableType[] d = Resources.LoadAll<OwnableType>("OwnableTypes");
        foreach (OwnableType o in d)
        {
            items.Add(o.OwnableTypeName, o);
        }
    }


}
