using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ExitCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;


    SkydiveManager skydiveManager;
    // Start is called before the first frame update
    void Start()
    {   
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        skydiveManager = FindObjectOfType<SkydiveManager>();
        skydiveManager.OnAircraftSet += HandleAircraftSet;
    }

    private void HandleAircraftSet(AircraftInstance obj)
    {
        virtualCamera.Follow = obj.transform;
        virtualCamera.LookAt = obj.transform;
    }
}
