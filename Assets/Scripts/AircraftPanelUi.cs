using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class AircraftPanelUi : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    TMP_Text label;

    [SerializeField]
    TMP_Text amountLabel;

    [SerializeField]
    AircraftSelector aircraftSelector;

    void Awake()
    {

        aircraftSelector.OnAircraftSelected += SetAircraftType;

    }


    public void SetAircraftType(AircraftType aircraftType)
    {
        image.sprite = aircraftType.GetSprite();
        label.text = aircraftType.AircraftTypeName;
        amountLabel.text = aircraftType.AmountOfSeats.ToString();
    }

}
