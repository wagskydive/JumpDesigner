using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JumpSequenceSelector : MonoBehaviour
{
    public event Action<JumpSequence> OnSequenceSelected;

    public JumpSequence SelectedSequence { get => currentSequences[dropdown.value]; }

    public TMP_Dropdown dropdown;

    List<JumpSequence> allSequences;

    List<JumpSequence> filteredSequences;


    List<JumpSequence> currentSequences;

    [SerializeField]
    public AmountSetter jumpAmountSetter;


    int jumperAmountFilter = 1;


    public void SetJumpAmountFilter(int jumperAmount)
    {
        jumperAmountFilter = jumperAmount;
        PopulateDropdown();
    }

    private void Awake()
    {
        GetSequences();
        PopulateDropdown(allSequences);
        if(jumpAmountSetter != null)
        {
            jumpAmountSetter.OnAmountChanged += SetJumpAmountFilter;
        }

    }
    private void OnEnable()
    {
        PopulateDropdown(allSequences);
    }
    public void HandleSelection()
    {
        OnSequenceSelected?.Invoke(SelectedSequence);
    }

    void GetSequences()
    {
        allSequences = new List<JumpSequence>();

        allSequences.AddRange(FileHandler.ReadInternalJumps());

        allSequences.AddRange(FileHandler.ReadSavedJumps());
    }

    void PopulateDropdown(List<JumpSequence> sequences = null)
    {
        if(sequences == null)
        {
            GetSequences();
            sequences = allSequences;
        }

        if(jumperAmountFilter != 0)
        {
            sequences = FilterSequences(sequences);
        }
        dropdown.ClearOptions();
        
        List<string> names = new List<string>();
        for (int i = 0; i < sequences.Count; i++)
        {
            names.Add(sequences[i].JumpName);
        }
        dropdown.AddOptions(names);
        currentSequences = sequences;
    }


    List<JumpSequence> FilterSequences(List<JumpSequence> sequences)
    {
        List<JumpSequence> filtered = new List<JumpSequence>();

        for (int i = 0; i < sequences.Count; i++)
        {
            if (sequences[i].TotalSkydivers() == jumperAmountFilter)
            {
                filtered.Add(sequences[i]);
            }
        }

        return filtered;
    }
}
