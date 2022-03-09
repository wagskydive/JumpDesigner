using System;
using UnityEngine;

public class OwnedItem
{
    public static event Action<OwnedItem, Transform> OnEquip;
    public static event Action<OwnedItem, Transform> OnUnEquip;


    public OwnableType TypeOfOwnable { get; private set; }
    public OwnershipSpecs Specs { get; private set; }

    public OwnedItem(OwnableType ownableType, OwnershipSpecs ownershipSpecs)
    {
        TypeOfOwnable = ownableType;
        Specs = ownershipSpecs;
    }



    public void Equip(Transform parent)
    {
        OnEquip?.Invoke(this, parent);
    }

    public void UnEquip(Transform parent)
    {
        OnUnEquip?.Invoke(this, parent);
    }

}
