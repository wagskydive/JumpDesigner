using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSelector : MonoBehaviour
{
    public AircraftType selectedAircraft;

    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;

    public event Action<AircraftType> OnAircraftSelected;

    int currentIndex = 0;

    List<AircraftType> allAircraftTypes;

    List<AircraftType> filteredAircraftTypes;

    void Awake()
    {
        jumpSequenceSelector.OnSequenceSelected += HandleJumpSequence;
        jumpSequenceSelector.jumpAmountSetter.OnAmountChanged += FilterAircraftTypes;
    }


    private void HandleJumpSequence(JumpSequence jumpSequence)
    {
        int skydiverAmount = jumpSequence.TotalSkydivers();

        FilterAircraftTypes(skydiverAmount);
    }



    void FilterAircraftTypes(int skydiverAmount)
    {
        filteredAircraftTypes = new List<AircraftType>();

        foreach (AircraftType aircraftType in allAircraftTypes)
        {
            if (aircraftType.AmountOfSeats >= skydiverAmount)
            {
                filteredAircraftTypes.Add(aircraftType);
            }
        }
        if(selectedAircraft.AmountOfSeats < skydiverAmount)
        {
            SelectAircraftType(filteredAircraftTypes[filteredAircraftTypes.Count - 1]);
        }

        
    }

    void Start()
    {
        GetListOfAircraftTypes();
        filteredAircraftTypes = allAircraftTypes;
        SelectAircraftType(allAircraftTypes[currentIndex]);

    }

    void GetListOfAircraftTypes()
    {
         allAircraftTypes = FileHandler.LoadAircraftTypes();
    }



    void SelectAircraftType(AircraftType aircraft)
    {
        selectedAircraft = aircraft;
        OnAircraftSelected?.Invoke(selectedAircraft);
    }

    public void SelectNextAircraft()
    {
        currentIndex++;
        if (currentIndex >= filteredAircraftTypes.Count)
        {
            currentIndex = 0;
        }
        SelectAircraftType(filteredAircraftTypes[currentIndex]);
    }

    public void SelectPreviousAircraft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = filteredAircraftTypes.Count - 1;
        }
        SelectAircraftType(filteredAircraftTypes[currentIndex]);
    }



}
