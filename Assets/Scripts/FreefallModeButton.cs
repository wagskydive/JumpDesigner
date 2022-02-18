using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreefallModeButton : MonoBehaviour
{
    public void FreefallModeButtonPress()
    {
        FindObjectOfType<GameLoader>().FreefallButtonPress();
    }
}
