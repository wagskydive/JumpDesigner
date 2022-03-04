using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpSequenceInfo : MonoBehaviour
{
    TMP_Text text;

    [SerializeField]
    JumpSequenceSelector sequenceSelector;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        sequenceSelector.OnSequenceSelected += SetTextFromSequence;
        SetTextFromSequence(sequenceSelector.SelectedSequence);
    }

    public void SetTextFromSequence(JumpSequence jumpSequence)
    {
        string details = "";
        details += jumpSequence.JumpName + "\n";
        details += "Group Size: " + jumpSequence.TotalSkydivers().ToString() + "\n";

        for (int i = 0; i < jumpSequence.DiveFlow.Count; i++)
        {

            details += "Base Orientation: " + jumpSequence.DiveFlow[i].BaseOrientation.ToString() + "\n" + "\n";
            //details += "Amount: " + jumpSequence.DiveFlow[i].AmountOfJumpers.ToString();
            for (int j = 0; j < jumpSequence.DiveFlow[i].FormationSlots.Count; j++)
            {
                SkydiveFormationSlot slot = jumpSequence.DiveFlow[i].FormationSlots[j];
                details += "Slot: " + (j + 1).ToString() + "\n";
                details += "Rotation: " +slot.BaseRotation.ToString() + "\n";
                details += "Orientation: "+slot.Orientation.ToString() + "\n";
                details += "Index: "+slot.SkydiverIndex.ToString() + "\n";
                details += "Slot Position: " +slot.Slot.ToString() + "\n";
                details += "Target Index: " +slot.TargetIndex.ToString() + "\n" + "\n";
            }          
        }
        

        text.text = details;
    }
}
