using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        SelectionHandler.OnSelected += SetCameraAim;
        SelectionHandler.OnTakeControlConfirmed += SetCameraTargetAndFollow;
        SelectionHandler.OnDeselected += ResetCameraAim;
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
