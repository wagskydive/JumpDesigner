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

        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0, 4, -4);
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
        SetCameraAim(obj);
        vcam.Follow = obj.transform;
        obj.transform.GetComponent<MovementController>().OnPull += PullHandle;
    }

    void PullHandle()
    {
        Invoke("SetCanopyCamera", 1.4f);
    }

    private void SetCanopyCamera()
    {
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0, 4, -20);
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
