using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotUi : MonoBehaviour
{
    public event Action<CharacterSlotUi> OnSlotClicked;

    public CharacterData characterData;

    [SerializeField]
    Image icon;

    

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
        icon.sprite = characterData.GetSprite();
    }

    public void ButtonClick()
    {
        OnSlotClicked?.Invoke(this);
    }


}
