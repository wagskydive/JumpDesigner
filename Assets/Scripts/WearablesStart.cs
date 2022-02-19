using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearablesStart : MonoBehaviour
{
    [SerializeField]
    GameObject[] startSets;

    [SerializeField]
    WearblesController wearblesController;
    private void Start()
    {
        //StatsDistributor.OnNewCurrentHighest += ProcessClothes;
    }

    void ProcessClothes(int index)
    {
        GameObject[] clothes = new GameObject[startSets[index].transform.childCount];
        for (int i = 0; i < clothes.Length; i++)
        {
            clothes[i] = startSets[index].transform.GetChild(i).gameObject;
        }
        wearblesController.SetNewClothes(clothes);
    }


}
