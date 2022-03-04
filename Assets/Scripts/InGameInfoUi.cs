using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameInfoUi : MonoBehaviour
{
    [SerializeField]
    TMP_Text jumpSequenceName;

    [SerializeField]
    TMP_Text indexText;

    SkydiveManager skydiveManager;

    private void Start()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        skydiveManager.OnPlaybackStarted += HandlePlaybackStart;
        skydiveManager.OnNextFormationSet += HandleNextFormation;
    }

    private void HandleNextFormation(int index)
    {
        indexText.text = "Index: " + index.ToString();
    }

    private void HandlePlaybackStart(JumpSequence sequence)
    {
        jumpSequenceName.text = sequence.JumpName;
    }
}
