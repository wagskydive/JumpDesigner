using UnityEngine;

public class AircraftType
{
    string aircraftTypeName;

    string fileName;

    float movingSpeed;

    int amountOfSeats;

    

    public string AircraftTypeName { get =>aircraftTypeName; }

    public string FileName { get => fileName; }

    public float MovingSpeed { get => movingSpeed; }

    public int AmountOfSeats { get => amountOfSeats; }

    public AircraftType(string _name, string _fileName, float _movingSpeed, int _amountOfSeats)
    {
        aircraftTypeName = _name;
        fileName = _fileName;
        movingSpeed = _movingSpeed;
        amountOfSeats = _amountOfSeats;
    }

    public GameObject GetPrefabInstance()
    {

        GameObject prefabInstance = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Aircrafts/" + fileName));
        AircraftInstance aircraft = prefabInstance.GetComponent<AircraftInstance>();

        if(aircraft == null)
        {
            Debug.LogError("Prefab " + fileName + " does not contain an Aircraft component");
        }

        prefabInstance.GetComponent<AircraftInstance>().SetAircraftType(this);
        return prefabInstance;
    }   

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Art/Icons/AircraftIcons/" + fileName+"Icon512");
    }

}
