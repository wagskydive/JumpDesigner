using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpModeButton : MonoBehaviour
{
    public void JumpModeButtonPress()
    {
        FindObjectOfType<GameLoader>().JumpButtonPress();
    }
}
