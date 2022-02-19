using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearblesController : MonoBehaviour
{
    GameObject[] currentWearables;

    // Start is called before the first frame update
    void Start()
    {
        currentWearables = new GameObject[transform.childCount];
        for (int i = 0; i < currentWearables.Length; i++)
        {
            currentWearables[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetNewClothes(GameObject[] newClothes)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int j = 0; j < newClothes.Length; j++)
        {
            Instantiate(newClothes[j], transform).SetActive(true);
        }
    }

}
