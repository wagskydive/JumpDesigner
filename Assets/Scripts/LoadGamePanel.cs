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

    void Awake()
    {
        aircraftSelector.OnAircraftSelected += SetAircraftType;

    }
    private void Start()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();

    }

    private void SetAircraftType(AircraftType aircraftType)
    {
        currentAircraftType = aircraftType;
    }


    public void PlayButtonPress()
    {
        altitude = heightSetter.CurrentAmount / 3.28f;
        if(skydiveManager.aircraft != null)
        {
            Destroy(skydiveManager.aircraft.gameObject);
        }

        
        skydiveManager.SetAircraft(currentAircraftType.GetPrefabInstance().GetComponent<AircraftInstance>());
        

        skydiveManager.SetupJumpRun(jumpSequenceSelector.SelectedSequence,Mathf.RoundToInt(altitude));
        
        if (infintyToggle.isOn)
        {
            terrain.SetActive(false);
        }
        else
        {
            terrain.SetActive(true);
        }
        transform.root.gameObject.SetActive(false);
    }


    


    

    public void DismissButtonPress()
    {
        transform.root.gameObject.SetActive(false);
    }
}
