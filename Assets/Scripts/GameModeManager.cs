using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public event Action OnResetButtonPressed;

    public void ResetButtonPressed()
    {
        OnResetButtonPressed.Invoke();
    }

    public void FollowMeModeButtonPressed()
    {


        SkydiveManager skydiveManager = FindObjectOfType<SkydiveManager>();


        if (skydiveManager.SpawnedSkydivers.Count > 0)
        {
            for (int i = 0; i < skydiveManager.SpawnedSkydivers.Count; i++)
            {
                if (skydiveManager.SpawnedSkydivers[i] != SelectionHandler.Instance.player)// && skydiveManager.SpawnedSkydivers[i].GetType() == typeof(NPC_Ai_FromState))
                {
                    NPC_Ai_FromState ai = skydiveManager.SpawnedSkydivers[i].transform.GetComponent<NPC_Ai_FromState>();
                    ai.SetState(new SkydiveState(FreefallOrientation.Belly, SelectionHandler.Instance.player));
                }
            }
        }
    }
}
