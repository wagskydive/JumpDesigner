using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    private void Awake()
    {
        Restart();
    }

    public void Restart()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("LoadScene"));
        SceneManager.LoadScene("FreefallScene");

    }
}
