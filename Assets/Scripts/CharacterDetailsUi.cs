using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterDetailsUi :MonoBehaviour, IPointerClickHandler
{
    public event Action OnDetailsClicked;

    public event Action<CharacterData> OnCharacterSelectionConfirmed;

    public CharacterData characterData;

    [SerializeField]
    TMP_Text nameText, genderText, occupationText, talentText;



    void Redraw()
    {
        nameText.text = characterData.Name;
        genderText.text = characterData.Gender;
        occupationText.text = characterData.Occupation;
        talentText.text = characterData.Talent.ToString();
    }

    public void SetCharacterData(CharacterData character)
    {
        characterData =character;
        Redraw();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnDetailsClicked?.Invoke();
    }

    public void ConfirmButtonClick()
    {
        OnCharacterSelectionConfirmed?.Invoke(characterData);
    }

}
