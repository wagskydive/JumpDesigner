using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    public JumperAmount jumperAmount;

    int currentJumpers = 0;

    //ItemDatabase itemDatabase;

    private void Awake()
    {
        if (FindObjectsOfType<GameLoader>().Length > 1)
        {
            Destroy(this);
        }
        
        //itemDatabase = new ItemDatabase();
        

        DontDestroyOnLoad(this.gameObject);
        //FileHandler.WriteJumpSequenceToFile(JumpCreator.FourWayTestJump(FreefallOrientation.Back));
        //FileHandler.WriteJumpSequenceToFile(JumpCreator.FourWayTestJump(FreefallOrientation.Belly));
        //FileHandler.WriteJumpSequenceToFile(JumpCreator.DefaultJump(4,FreefallOrientation.Back));
    }

    public void DesignerButtonPressed()
    {
        SceneManager.LoadScene("UiJumpSequence");
    }

    public void CanopyButtonPress()
    {
        SceneManager.LoadScene("CanopyScene");
    }
        
    public void CharacterButtonPress()
    {
        SceneManager.LoadScene("CharacterScene");
    }

    public void FreefallButtonPress()
    {
        SceneManager.LoadScene("FreefallScene");
    }

    public void JumpButtonPress()
    {
        currentJumpers = jumperAmount.CurrentAmount;
        LoadJumpScene(currentJumpers);
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
            FindObjectOfType<SkydiveManager>().StartDefaultJump(currentJumpers);

        }
        SceneManager.sceneLoaded -= SetSkydiveData;
    }
}
