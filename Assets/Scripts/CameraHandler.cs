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

        skydiveManager = FindObjectOfType<SkydiveManager>();
        skydiveManager.OnPlaybackStarted += HandlePlayback;
        skydiveManager.OnJumpRunSet += HandleJumpRun;
        FindObjectOfType<SkydiveSpawner>().OnSkydiverSpawned += SetCameraTargetAndFollow;

        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0, 4, -4);
        //skydiveManager = FindObjectOfType<SkydiveManager>();
        //skydiveManager.OnPlaybackStarted += HandlePlayback;
    }

    private void HandleJumpRun()
    {
        vcam.Priority -= 5;
    }

    private void HandlePlayback(JumpSequence sequence)
    {
        vcam.Priority += 5;
        vcam.LookAt = skydiveManager.SpawnedSkydivers[0].transform;
        vcam.Follow = skydiveManager.SpawnedSkydivers[0].transform;
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
        CinemachineTransposer transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
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
