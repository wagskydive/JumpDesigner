using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour
{
    [SerializeField]
    float distance;
    [SerializeField]
    int amount;

    GameObject[] clouds;

    GameObject cloudPrefab;

    private void Awake()
    {
        clouds = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            clouds[i] = Instantiate(Resources.Load("Prefab/CloudBackground") as GameObject);
        }
        
    }
    private void Update()
    {
        
    }
}
