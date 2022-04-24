using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataHandler : MonoBehaviour
{
    CharacterData characterData;

    public CharacterData CharacterData {get => characterData; }

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;

    }


    void AdjustAppearance()
    {
        
    }
}
