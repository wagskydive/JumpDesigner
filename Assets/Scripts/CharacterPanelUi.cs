using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class CharacterPanelUi : MonoBehaviour
{
    public event Action<CharacterSlotUi> OnCharacterSlotClicked;

    [SerializeField]
    public GameObject CharacterSlotPrefab;

    List<CharacterData> characterDatas;

    void Start()
    {
        //CreateRandomCharacters(20);
        Redraw();

    }

    private void CreateRandomCharacters(int amount)
    {
        characterDatas = FileHandler.LoadAllCharacters().ToList();



        //characterDatas = new List<CharacterData>();
        for (int i = 0; i < amount; i++)
        {

            ///characterDatas.Add(RandomCharacterGenerator.CreateRandomCharacter());
        }
        //FileHandler.SaveAllCharacters(characterDatas.ToArray());
    }

    public void SetCharacters(CharacterData[] characters)
    {
        characterDatas = characters.ToList();
        Redraw();
    }

    private void HandleSlotClicked(CharacterSlotUi obj)
    {
        OnCharacterSlotClicked?.Invoke(obj);
    }

    void Redraw()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        if(characterDatas != null)
        {
            foreach (CharacterData characterData in characterDatas)
            {
                GameObject characterSlot = Instantiate(CharacterSlotPrefab, transform);
                characterSlot.GetComponent<CharacterSlotUi>().SetCharacterData(characterData);
                characterSlot.GetComponent<CharacterSlotUi>().OnSlotClicked += HandleSlotClicked;
            }
        }

    }

    public CharacterSlotUi SlotUiFromCharacterData(CharacterData characterData)
    {

        CharacterSlotUi[] slots = transform.GetComponentsInChildren<CharacterSlotUi>();
        foreach (CharacterSlotUi slot in slots)
        {
            if (slot.characterData == characterData)
            {
                return slot;
            }
        }
        return null;
    }


}
