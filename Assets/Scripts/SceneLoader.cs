using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    GameObject startUi;

    [SerializeField]
    int amountToLoad;


    public void InputFieldChange(string input)
    {
        amountToLoad = int.Parse(input);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += HandleSceneLoad;
    }

    public void LoadSceneButtonPress()
    {
        if(amountToLoad > 0)
        {
            SceneManager.LoadScene("FreefallScene");
        }

          
    }

    private void HandleSceneLoad(Scene scene, LoadSceneMode arg1)
    {
        if(scene== SceneManager.GetSceneByName("FreefallScene"))
        {
            SceneManager.SetActiveScene(scene);
            FindObjectOfType<SkydiveSpawner>().SpawnSkydivers(amountToLoad);
            startUi.SetActive(false);
        }

    }
}
