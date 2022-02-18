using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    SkydiveManager skydiveManager;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        SelectionHandler.OnSelected += SetCameraAim;
        SelectionHandler.OnTakeControlConfirmed += SetCameraTargetAndFollow;
        SelectionHandler.OnDeselected += ResetCameraAim;
        FindObjectOfType<SkydiveManager>().OnPlaybackStarted += HandlePlayback;
        FindObjectOfType<SkydiveSpawner>().OnSkydiverSpawned += SetCameraTargetAndFollow;

        //skydiveManager = FindObjectOfType<SkydiveManager>();
        //skydiveManager.OnPlaybackStarted += HandlePlayback;
    }

    private void HandlePlayback()
    {
        vcam.LookAt = skydiveManager.middlepointNPCS;
        vcam.Follow = skydiveManager.middlepointNPCS;
    }

    private void SetCameraTargetAndFollow(ISelectable obj)
    {
        vcam.LookAt = obj.transform;
        vcam.Follow = obj.transform;
    }

    private void ResetCameraAim(ISelectable obj)
    {
        vcam.LookAt = vcam.Follow;
    }

    private void SetCameraAim(ISelectable obj)
    {
        vcam.LookAt = obj.transform;
    }
}
