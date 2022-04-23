using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 5;
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * movementSpeed);
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
}
