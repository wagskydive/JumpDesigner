using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftMovement : MonoBehaviour
{
    float movementSpeed = 5;
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * movementSpeed);
    }
}
