using UnityEngine;

[RequireComponent(typeof(AircraftMovement),typeof(AircraftTransforms))]
public class Aircraft : MonoBehaviour
{
    AircraftType aircraftType;

    public AircraftType AircraftType { get => aircraftType; }
    

    AircraftTransforms aircraftTransforms;

    public AircraftTransforms AircraftTransforms{ get => aircraftTransforms; }


    AircraftMovement aircraftMovement;


    void Start()
    {
        aircraftTransforms = GetComponent<AircraftTransforms>();
        aircraftMovement = GetComponent<AircraftMovement>();
    }



    public void SetAircraftType(AircraftType aircraftType)
    {
        this.aircraftType = aircraftType;
        aircraftMovement.SetMovementSpeed(aircraftType.MovingSpeed);
    }



}
