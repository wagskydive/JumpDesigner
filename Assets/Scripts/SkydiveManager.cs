using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkydiveManager : MonoBehaviour
{
    public event Action<ISelectable> OnSkydiverAdded;
    public List<ISelectable> SpawnedSkydivers = new List<ISelectable>();

    SkydiveSpawner spawner;

    [SerializeField]
    public Transform middlepointNPCS;

    private void Awake()
    {
        spawner = FindObjectOfType<SkydiveSpawner>();
    }

    public void SelectAllButtonPress()
    {
        if(!SelectionHandler.Instance.selectedList.SequenceEqual(SpawnedSkydivers))
        {
            SelectionHandler.Instance.SetSelectionList(new List<ISelectable>(SpawnedSkydivers));
        }
        
    }

    public void AddSkydiverButtonPress()
    {
        ISelectable skydiver = spawner.SpawnSkydiverWithAi().GetComponent<ISelectable>();
        SpawnedSkydivers.Add(skydiver);
        OnSkydiverAdded?.Invoke(skydiver);
    }


    public void RemoveSkydiverButtonPress()
    {
        if(SpawnedSkydivers.Count > 1)
        {
            ISelectable skydiverToDestroy = SpawnedSkydivers[SpawnedSkydivers.Count - 1];
            SpawnedSkydivers.RemoveAt(SpawnedSkydivers.Count - 1);
            Destroy(skydiverToDestroy.transform.gameObject);
        }        
    }

    public Vector3 GetAveragePosition()
    {
        if (SpawnedSkydivers.Count > 0)
        {
            int playerControlled = 0;
            Vector3 running = Vector3.zero;
            for (int i = 0; i < SpawnedSkydivers.Count; i++)
            {
                if (SpawnedSkydivers[i].transform.GetComponent<MovementController>().inputSource.GetType() != typeof(AccelAndTouchUiControl))
                {
                    running += SpawnedSkydivers[i].transform.position;
                }
                else
                {
                    playerControlled = 1;
                }
                
            }
            return running / (SpawnedSkydivers.Count- playerControlled);
        }
        else
        {
            return Vector3.zero;
        }
    }

    void SetAllToOrientation(FreefallOrientation freefallOrientation)
    {
        for (int i = 0; i < SpawnedSkydivers.Count; i++)
        {
            //SpawnedSkydivers[i].transform.GetComponent<MovementController>().tr
        }
    }

    private void FixedUpdate()
    {
        if (SpawnedSkydivers.Count > 0)
        {
            middlepointNPCS.position = GetAveragePosition();
        }
    }
}
