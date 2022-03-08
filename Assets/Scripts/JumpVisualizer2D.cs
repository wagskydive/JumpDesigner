using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public static class JumpSequenceHelper
{
    public static void MoveSkydiverInFormation(JumpSequence sequence, int skydiver, int receiver, int slot, int formationIndex)
    {
        Formation formation = sequence.DiveFlow[formationIndex];
        SkydiveFormationSlot oldSlot = sequence.DiveFlow[formationIndex].FormationSlots[skydiver - 1];

        SkydiveFormationSlot newSlot = new SkydiveFormationSlot(skydiver, oldSlot.Orientation, receiver, slot, oldSlot.BaseRotation);
        sequence.DiveFlow[formationIndex].FormationSlots[skydiver - 1] = newSlot;
    }
}

public class JumpVisualizer2D : MonoBehaviour
{
    [SerializeField]
    GameObject slotPrefab;

    JumpSequence jumpSequence;

    GameObject[] formationMembers;
    [SerializeField]
    TMP_Text formationIndicatorText;

    [SerializeField]
    TMP_Text nameText;

    int currentFormation;
    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;


    public void SaveWithName(string name)
    {
        jumpSequence.SetJumpName(name);
        FileHandler.WriteJumpSequenceToFile(jumpSequence);
    }

    private void Start()
    {
        //jumpSequence = FileHandler.ReadSavedJumps()[1];
        //ReDraw();
        SelectionHandler2D.OnOrientationChange += HandleOrientationChange;
        SelectionHandler2D.OnRotationChange += HandleRotationChange;
        SelectionHandler2D.OnMoveToRequest += HandleMoveRequest;
        jumpSequenceSelector.OnSequenceSelected += LoadSequence;
        SkydiveManager skydiveManager = FindObjectOfType<SkydiveManager>();
        if(skydiveManager != null)
        {
            skydiveManager.OnNextFormationSet += SetFormation;
        }
    }

    private void SetFormation(int index)
    {
        currentFormation = index;
        ReDraw();
    }

    public void AddSkydiver()
    {
        int currentAmount = jumpSequence.TotalSkydivers();
        Formation formation = jumpSequence.DiveFlow[currentFormation];
        formation.AddSlot(new SkydiveFormationSlot(currentAmount, formation.BaseOrientation, 0, 1, 0));
        ReDraw();
    }

    private void HandleMoveRequest(int selected, int clickedIndex, int slotIndex)
    {
        JumpSequenceHelper.MoveSkydiverInFormation(jumpSequence, selected, clickedIndex, slotIndex, currentFormation);
        ReDraw();
    }

    private void LoadSequence(JumpSequence seq)
    {
        
        jumpSequence = seq;
        currentFormation = 0;
        
        ReDraw();
    }

    public void SelectOtherFormation(int change)
    {
        if(jumpSequence != null)
        {
            currentFormation += change;
            if (currentFormation < 0)
            {
                currentFormation = jumpSequence.DiveFlow.Count - 1;
            }
            if(currentFormation > jumpSequence.DiveFlow.Count - 1)
            {
                currentFormation = 0;
            }
            ReDraw();
        }

    }

    public void AddFormation()
    {
        if(jumpSequence != null)
        {
            Formation formation = new Formation(jumpSequence.DiveFlow[jumpSequence.DiveFlow.Count - 1].BaseOrientation);
            for (int i = 0; i < jumpSequence.DiveFlow[jumpSequence.DiveFlow.Count - 1].FormationSlots.Count; i++)
            {
                SkydiveFormationSlot fmSl = jumpSequence.DiveFlow[jumpSequence.DiveFlow.Count - 1].FormationSlots[i];
                formation.AddSlot(new SkydiveFormationSlot(fmSl.SkydiverIndex, fmSl.Orientation, fmSl.TargetIndex, fmSl.Slot, fmSl.BaseRotation));
            }
            jumpSequence.AddFormation(formation);
            ReDraw();
        }

    }

    private void HandleRotationChange(int skydiverIndex, float rotationChange)
    {
        if(skydiverIndex != 0)
        {
            SkydiveFormationSlot skydiveFormationSlot = jumpSequence.DiveFlow[currentFormation].FormationSlots[skydiverIndex - 1];

            float newRotation = skydiveFormationSlot.BaseRotation + rotationChange;
            if(newRotation > 180)
            {
                newRotation -= 360;
            }
            if(newRotation < -180)
            {
                newRotation += 360;
            }
                        jumpSequence.DiveFlow[currentFormation].FormationSlots[skydiverIndex - 1] = new SkydiveFormationSlot(skydiveFormationSlot.SkydiverIndex, skydiveFormationSlot.Orientation, skydiveFormationSlot.TargetIndex, skydiveFormationSlot.Slot, newRotation);
            ReDraw();
        }
    }

    private void HandleOrientationChange(int skydiverIndex, FreefallOrientation newOrientation)
    {
        if(skydiverIndex == 0)
        {
            jumpSequence.DiveFlow[currentFormation].SetBaseOrientation(newOrientation);
        }
        else
        {
            SkydiveFormationSlot skydiveFormationSlot = jumpSequence.DiveFlow[currentFormation].FormationSlots[skydiverIndex - 1];
            jumpSequence.DiveFlow[currentFormation].FormationSlots[skydiverIndex - 1] = new SkydiveFormationSlot(skydiveFormationSlot.SkydiverIndex,newOrientation,skydiveFormationSlot.TargetIndex,skydiveFormationSlot.Slot,skydiveFormationSlot.BaseRotation);
        }
        ReDraw();
    }

    void ReDraw()
    {
        
        if(jumpSequence != null)
        {
            Formation formation = jumpSequence.DiveFlow[currentFormation];
            GameObject formationObject = DrawFormation(jumpSequence.DiveFlow[currentFormation]);
            formationObject.transform.SetParent(transform);
            formationObject.transform.localPosition = Vector3.zero;

            formationIndicatorText.text = "Formation: " + (currentFormation + 1).ToString() + " / " + jumpSequence.DiveFlow.Count.ToString();

            nameText.text = jumpSequence.JumpName;
        }
    }

    public void NewJumpButtonPress()
    {
        jumpSequence = new JumpSequence("New Jump");
        Formation formation = new Formation(FreefallOrientation.Belly);

        jumpSequence.AddFormation(formation);
        ReDraw();
    }

    GameObject DrawFormation(Formation formation)
    {
        if(formationMembers == null || formationMembers.Length != formation.AmountOfJumpers)
        {
            ClearChildren();
            formationMembers = new GameObject[formation.AmountOfJumpers];

            for (int i = 0; i < formation.AmountOfJumpers; i++)
            {
                formationMembers[i] = Instantiate(slotPrefab);
            }
        }


        for (int i = 0; i < formation.AmountOfJumpers; i++)
        {
            if (i > 0)
            {
                formationMembers[i].transform.SetParent(formationMembers[formation.FormationSlots[i - 1].TargetIndex].GetComponent<Slot2D>().childSlotParents[formation.FormationSlots[i - 1].Slot]);
                formationMembers[i].transform.localPosition = Vector3.zero;
                
            }

            formationMembers[i].GetComponent<Slot2D>().SetVisual(formation, i);
        }


        formationMembers[0].transform.transform.localPosition = Vector3.zero;
        return formationMembers[0];
    }


    private void ClearChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
