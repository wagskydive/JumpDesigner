using UnityEngine;

[RequireComponent(typeof(AircraftMovement),typeof(AircraftTransforms))]
public class AircraftInstance : MonoBehaviour
{
    AircraftType aircraftType;

    public AircraftType AircraftType { get => aircraftType; }
    

    AircraftTransforms aircraftTransforms;

    public AircraftTransforms AircraftTransforms{ get => GetAircraftTransforms(); }


    AircraftMovement aircraftMovement;
    

    AircraftTransforms GetAircraftTransforms()
    {
        if (aircraftTransforms == null)
        {
            aircraftTransforms = GetComponent<AircraftTransforms>();
        }
        return aircraftTransforms;
    }



    void Start()
    {
        aircraftTransforms = GetComponent<AircraftTransforms>();
        aircraftMovement = GetComponent<AircraftMovement>();

        if(aircraftType != null)
        {
            aircraftMovement.SetMovementSpeed(aircraftType.MovingSpeed);
        }
    }



    public void SetAircraftType(AircraftType aircraftType)
    {
        this.aircraftType = aircraftType;

    }



}
