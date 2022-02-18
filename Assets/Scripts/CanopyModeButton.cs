using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyModeButton : MonoBehaviour
{
    public void CanopyModeButtonPressed()
    {
        FindObjectOfType<GameLoader>().CanopyButtonPress();
    }

}
