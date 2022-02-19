using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModeButton : MonoBehaviour
{
    public void CharacterButtonPress()
    {
        FindObjectOfType<GameLoader>().CharacterButtonPress();
    }

}
