using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydiveSpawner : MonoBehaviour
{
    public event Action<ISelectable> OnSkydiverSpawned;
    [SerializeField]
    Transform spawnPoint;


    private void Awake()
    {
        //SelectionHandler.OnSelected += SetFollow;
        //spawnPoint = transform;
    }

    private void SetFollow(ISelectable obj)
    {
        //spawnPoint = obj.transform;
        //transform.SetParent(obj.transform);
        //transform.localPosition = Vector3.zero;

    }


    public List<GameObject> SpawnSkydivers(int amount)
    {
        List<GameObject> spawned = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject go = SpawnSkydiverWithAi(i);
            spawned.Add(go);
        }
        return spawned;
    }

    public GameObject SpawnSkydiverWithAi(int index,FreefallOrientation orientation = FreefallOrientation.Belly)
    {
        GameObject skydiver = Instantiate(Resources.Load("Prefabs/SkydiveCharacter") as GameObject, spawnPoint.position, Quaternion.identity);
        NPC_Ai_FromState aiInput = skydiver.AddComponent<NPC_Ai_FromState>();
        aiInput.SetIndex(index);
        skydiver.GetComponent<MovementController>().ReplaceInput(aiInput);

        OnSkydiverSpawned?.Invoke(skydiver.GetComponent<Selectable>());
        return skydiver;
    }

    public Transform SpawnGhost(int index)
    {

        GameObject ghost = Instantiate(Resources.Load("Prefabs/GhostCharacter") as GameObject);
        ghost.GetComponent<GhostAnimationController>().SetSkydiverIndex(index);
        return ghost.transform;

    }
}
