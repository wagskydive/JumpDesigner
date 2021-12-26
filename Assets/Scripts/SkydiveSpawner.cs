using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydiveSpawner : MonoBehaviour
{


    private void Start()
    {
        SelectionHandler.OnSelected += SetFollow;
    }

    private void SetFollow(ISelectable obj)
    {
        transform.SetParent(obj.transform);
        transform.localPosition = Vector3.zero;

    }


    public List<GameObject> SpawnSkydivers(int amount)
    {
        List<GameObject> spawned = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject go = SpawnSkydiverWithAi();
            spawned.Add(go);
        }
        return spawned;
    }

    public GameObject SpawnSkydiverWithAi(FreefallOrientation orientation = FreefallOrientation.Belly)
    {
        GameObject skydiver = Instantiate(Resources.Load("Prefabs/SkydiveCharacter") as GameObject, transform.position, Quaternion.identity);
        NPC_Ai_FromState aiInput = skydiver.AddComponent<NPC_Ai_FromState>();
        skydiver.GetComponent<MovementController>().ReplaceInput(aiInput);

        return skydiver;
    }



}