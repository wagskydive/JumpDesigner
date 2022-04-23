using System.Collections;
using UnityEngine;

public class GearSpawner : MonoBehaviour
{
    private void Awake()
    {


    }

    private void EquipOwnable(OwnedItem ownable, Transform target)
    {
        GameObject instance = Instantiate(Resources.Load("Prefabs/Items/"+ownable.TypeOfOwnable.ResourceName) as GameObject,Vector3.zero,Quaternion.identity,target);
        OwnableInstance ownableInstance= instance.AddComponent<OwnableInstance>();
        ownableInstance.Item = ownable;
    }
}
