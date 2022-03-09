using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiDigitalAltimeter : MonoBehaviour
{
    TMP_Text altitudeText;

    [SerializeField]
    Transform character;
    [SerializeField]
    Transform terrain;

    private void Start()
    {
        altitudeText = GetComponent<TMP_Text>();
        SelectionHandler.OnSelected += SetCharacter;
        SkydiveManager skydiveManager = FindObjectOfType<SkydiveManager>();
        if(skydiveManager != null)
        {
            skydiveManager.OnExitStarted += HandleExit;
        }
        
    }

    private void HandleExit()
    {
        SkydiveManager skydiveManager = FindObjectOfType<SkydiveManager>();
        SetCharacter(skydiveManager.SpawnedSkydivers[0]);
    }

    private void SetCharacter(ISelectable obj)
    {
        character = obj.transform;
    }

    private void Update()
    {
        altitudeText.text = Mathf.Round((character.position.y - terrain.position.y)*3.28f).ToString();
    }
}
