using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadGamePanel : MonoBehaviour
{
    public event Action OnPlayJump;

    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;

    [SerializeField]
    AircraftSelector aircraftSelector;

    [SerializeField]
    AmountSetter heightSetter;

    SkydiveManager skydiveManager;

    [SerializeField]
    Toggle infintyToggle;
    [SerializeField]
    GameObject terrain;

    AircraftType currentAircraftType;

    float altitude;
    private void Start()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        aircraftSelector.OnAircraftSelected += SetAircraftType;
    }

    private void SetAircraftType(AircraftType aircraftType)
    {
        currentAircraftType = aircraftType;
    }


    public void PlayButtonPress()
    {
        altitude = heightSetter.CurrentAmount / 3.28f;

        
        skydiveManager.SetAircraft(CreateAircraft(currentAircraftType));
        

        skydiveManager.SetupJumpRun(jumpSequenceSelector.SelectedSequence,Mathf.RoundToInt(altitude));
        transform.root.gameObject.SetActive(false);
        if (infintyToggle.isOn)
        {
            terrain.SetActive(false);
        }
        else
        {
            terrain.SetActive(true);
        }
    }

    public Aircraft CreateAircraft(AircraftType aircraftType)
    {
        GameObject aircraftObject = GameObject.Instantiate(Resources.Load("Prefabs/Aircrafts/" + aircraftType.FileName), new Vector3(0,altitude,0),Quaternion.identity) as GameObject;
        Aircraft aircraft = aircraftObject.GetComponent<Aircraft>();
        aircraft.SetAircraftType(aircraftType);
        return aircraft;
    }
    


    

    public void DismissButtonPress()
    {
        transform.root.gameObject.SetActive(false);
    }
}
