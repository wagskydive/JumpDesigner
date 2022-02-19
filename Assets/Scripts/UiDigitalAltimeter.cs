using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    }

    private void Update()
    {
        altitudeText.text = Mathf.Round((character.position.y - terrain.position.y)*3.28f).ToString();
    }
}
