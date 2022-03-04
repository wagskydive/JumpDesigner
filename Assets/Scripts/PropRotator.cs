using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRotator : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 40;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
