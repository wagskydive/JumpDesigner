using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(CharacterPanelUi))]
public class CharacterSelectionUi : MonoBehaviour
{



  
    CharacterPanelUi characterPanelUi;

    SkydiveManager skydiveManager;

    CharacterSlotUi selectedSlot;

    [SerializeField]
    Color selectedColor;

    Color unselectedColor;

    [SerializeField]
    CharacterDetailsUi characterDetailsUi;


    void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();

        skydiveManager.OnPlaybackStarted += OnPlaybackStarted;
        characterPanelUi = GetComponent<CharacterPanelUi>();
        characterPanelUi.OnCharacterSlotClicked += SelectCharacterSlot;

        unselectedColor = characterPanelUi.CharacterSlotPrefab.GetComponent<Image>().color;
        if(characterDetailsUi != null)
        {
            characterDetailsUi.OnDetailsClicked += OnDetailsClicked;
            characterDetailsUi.OnCharacterSelectionConfirmed += CharacterSelectionConfirmed;
            SelectionHandler.OnSelected += OnSelectedFromSelectionHandler;
        }
    }

    private void OnSelectedFromSelectionHandler(ISelectable obj)
    {
        if(skydiveManager.GetSelectableFromCharacterData(characterDetailsUi.characterData) != obj)
        {


            SelectCharacterSlot(characterPanelUi.SlotUiFromCharacterData( obj.transform.GetComponent<CharacterDataHandler>().CharacterData));

        }
    }



    private void CharacterSelectionConfirmed(CharacterData obj)
    {


        
        SelectionHandler.Instance.TakeControOfSelection();
    }

    private void OnPlaybackStarted(JumpSequence obj)
    {

        characterPanelUi.SetCharacters(skydiveManager.GetCharacters());
        
    }

    private void SelectCharacterSlot(CharacterSlotUi obj)
    {
        if (selectedSlot != null)
        {
            Deselect();
        }
        selectedSlot = obj;
        selectedSlot.GetComponent<Image>().color = selectedColor;
        characterDetailsUi.SetCharacterData(obj.characterData);
        characterDetailsUi.gameObject.SetActive(true);
        SelectionHandler.Instance.SetSelection(skydiveManager.GetSelectableFromCharacterData(obj.characterData));
            
    }

    void Deselect()
    {
        if (selectedSlot != null)
        {
            selectedSlot.GetComponent<Image>().color = unselectedColor;
        }
        selectedSlot = null;
        characterDetailsUi.gameObject.SetActive(false);
    }

    void OnDetailsClicked()
    {
        Deselect();
    }

    void OnLooseControl()
    {
        SelectionHandler.Instance.LooseControlOfSelection();
    }
}
