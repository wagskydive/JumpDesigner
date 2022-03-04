using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpSaveWindow : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;

    [SerializeField]
    JumpVisualizer2D jumpVisualizer;

    public void ConfirmButtonPress()
    {
        if(inputField.text != "")
        {
            jumpVisualizer.SaveWithName(inputField.text);
        }
    }
}
