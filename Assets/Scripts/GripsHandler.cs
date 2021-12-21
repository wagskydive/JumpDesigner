using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripsHandler : MonoBehaviour
{
    [SerializeField]
    Grip[] gripSensors;

    private void Awake()
    {
        //for (int i = 0; i < gripSensors.Length; i++)
        //{
        //    gripSensors[i].OnSense += HandleGripSense;
        //}
    }

    private void HandleGripSense(Grip grip)
    {
        
    }
}
