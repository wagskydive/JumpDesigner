using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    JumperAmount jumperAmount;

    private void Awake()
    {
        if (FindObjectsOfType<GameLoader>().Length > 1)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void CanopyButtonPress()
    {
        SceneManager.LoadScene("CanopyScene");
    }

    public void FreefallButtonPress()
    {
        SceneManager.LoadScene("FreefallScene");
    }

    public void JumpButtonPress()
    {
        LoadJumpScene(jumperAmount.CurrentAmount);
    }

    void LoadJumpScene(int jumpers)
    {
        SceneManager.LoadScene("FreefallScene");
        SceneManager.sceneLoaded += SetSkydiveData;
    }

    public void StopButtonPressed()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void SetSkydiveData(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "FreefallScene")
        {
            FindObjectOfType<SkydiveManager>().StartDefaultJump(jumperAmount.CurrentAmount);

        }
        SceneManager.sceneLoaded -= SetSkydiveData;
    }
}
