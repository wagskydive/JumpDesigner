using UnityEngine;
using UnityEngine.SceneManagement;

public class StopButton : MonoBehaviour
{
    public void StopButtonPress()
    {
        FindObjectOfType<GameLoader>().StopButtonPressed();
        
    }
} 
