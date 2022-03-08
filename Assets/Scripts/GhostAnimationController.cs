﻿using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GhostAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform CharacterOffset;

    SkydiveManager skydiveManager;

    int skydiverIndex;

    Transform target;
    SkydiveFormationSlot slot;
    
    bool inPlayback;

    public void SetSkydiverIndex(int index)
    {
        skydiverIndex = index;
        
    }


    private void Start()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        if(skydiveManager == null)
        {
            gameObject.SetActive(false);
        }
        skydiveManager.OnPlaybackStarted += PlaybackStarted;
        skydiveManager.OnNextFormationSet += NextFormation;
        skydiveManager.OnJumpRunSet += PlaybackOnHold;

    }

    private void PlaybackOnHold()
    {
        inPlayback = false;
    }

    private void NextFormation(int index)
    {
        if (!inPlayback) { inPlayback = true; }
        if (skydiverIndex > 0)
        {
            slot = skydiveManager.CurrentJumpSequence.DiveFlow[index].FormationSlots[skydiverIndex - 1];

            target = skydiveManager.SpawnedGhosts[slot.TargetIndex];

            Vector3 targetWithSlotOffset = target.TransformPoint(SlotPositionHelper.SlotOffset(slot.Slot));

            transform.position = targetWithSlotOffset;
            SetRotation(target, slot.BaseRotation);
            Transition(slot.Orientation);
        }
        else
        {
            transform.position = skydiveManager.SpawnedSkydivers[0].transform.position;
            SetRotation(skydiveManager.SpawnedSkydivers[0].transform, 0);
            Transition(skydiveManager.CurrentJumpSequence.DiveFlow[index].BaseOrientation);
        }
    }

    private void PlaybackStarted(JumpSequence seq)
    {
        inPlayback = true;
        if(skydiverIndex > 0)
        {
            slot = seq.DiveFlow[0].FormationSlots[skydiverIndex - 1];

            target = skydiveManager.SpawnedGhosts[slot.TargetIndex];

            Vector3 targetWithSlotOffset = target.TransformPoint(SlotPositionHelper.SlotOffset(slot.Slot));

            transform.position = targetWithSlotOffset;
            SetRotation(target, slot.BaseRotation);
            Transition(slot.Orientation);
        }
        else
        {
            transform.position = skydiveManager.SpawnedSkydivers[0].transform.position;
            SetRotation(skydiveManager.SpawnedSkydivers[0].transform, 0);
            Transition(seq.DiveFlow[0].BaseOrientation);
            target = null;
            slot = null;
        }
        
    }

    private void Transition(FreefallOrientation orientation)
    {
        animator.SetFloat("Orientation", (int)orientation);
        SetOrientationCorrection((int)orientation);
    }

    private void SetOrientationCorrection(int orientationIndex)
    {
        CharacterOffset.localEulerAngles = new Vector3((orientationIndex * 90) - 180, 0, 0);
    }

    void SetRotation(Transform target, float rotation)
    {
        //Vector3 targetRotation = target.localEulerAngles;
        //float finalYRotation = targetRotation.y + rotation;
        //
        //
        //
        //Vector3 newForward = new Vector3(0, finalYRotation, 0);
        SetOrientationCorrection((int)animator.GetFloat("Orientation"));

        Quaternion newRotation = target.rotation;

        newRotation *= Quaternion.Euler(new Vector3(0, -rotation, 0));

        transform.rotation = newRotation;

    }

    private void FixedUpdate()
    {

        if (inPlayback)
        {
            if(skydiverIndex != 0 && slot != null && target != null)
            {
                Vector3 targetWithSlotOffset = target.TransformPoint(SlotPositionHelper.SlotOffset(slot.Slot));



                transform.position = new Vector3(targetWithSlotOffset.x,skydiveManager.SpawnedSkydivers[0].transform.position.y,targetWithSlotOffset.z) ;

                //transform.position = skydiveManager.FormationOffset(slot);


                SetRotation(target, slot.BaseRotation);
            }
            else if (skydiverIndex == 0)
            {
                transform.position = skydiveManager.SpawnedSkydivers[0].transform.position;
                transform.rotation = skydiveManager.SpawnedSkydivers[0].transform.rotation;

            }

            
        }
    }



   





}
