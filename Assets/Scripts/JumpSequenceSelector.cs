using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpSequenceSelector : MonoBehaviour
{
    public JumpSequence SelectedSequence { get; private set; }

    public TMP_Dropdown dropdown;

    List<JumpSequence> allSequences;

    private void Start()
    {
        GetSequences();
        PopulateDropdown();
    }

    void GetSequences()
    {
        allSequences = FileHandler.ReadSavedJumps();        
    }

    void PopulateDropdown()
    {
        dropdown.ClearOptions();
        List<string> names = new List<string>();
        for (int i = 0; i < allSequences.Count; i++)
        {
            names.Add(allSequences[i].JumpName);
        }
        dropdown.AddOptions(names);
    }
}
