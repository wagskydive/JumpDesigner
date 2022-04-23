using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftSelector : MonoBehaviour
{
    AircraftType selectedAircraft;

    

    public event Action<AircraftType> OnAircraftSelected;

    int currentIndex = 0;

    List<AircraftType> aircraftTypes;


    void Start()
    {
        GetListOfAircraftTypes();
        SelectAircraftType( aircraftTypes[currentIndex]);
    }

    void GetListOfAircraftTypes()
    {
         aircraftTypes = FileHandler.LoadAircraftTypes();
    }

    void SelectAircraftType(AircraftType aircraft)
    {
        selectedAircraft = aircraft;
        OnAircraftSelected?.Invoke(selectedAircraft);
    }

    public void SelectNextAircraft()
    {
        currentIndex++;
        if (currentIndex >= aircraftTypes.Count)
        {
            currentIndex = 0;
        }
        SelectAircraftType(aircraftTypes[currentIndex]);
    }

    public void SelectPreviousAircraft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = aircraftTypes.Count - 1;
        }
        SelectAircraftType(aircraftTypes[currentIndex]);
    }



}
