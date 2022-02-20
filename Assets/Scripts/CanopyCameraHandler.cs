using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CanopyCameraHandler : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    CinemachineTransposer transposer;

    bool active;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        AltitudeOverTerrain.OnTerrainHit += TerrainCamera;
        AltitudeOverTerrain.OnNoHit += NoHit;
    
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(35, 15, -20);
    }

    private void NoHit()
    {
        if (active)
        {
           
            Deactivate();
        }
    }

    private void Deactivate()
    {
        vcam.Priority = 0;
        active = false;
    }

    private void TerrainCamera(Vector3 point, float distance)
    {
        //transposer.m_FollowOffset = new Vector3(Mathf.Clamp(((200-distance)/200)*50,0,30), 10, Mathf.Clamp((20 - distance/10)-20, -20, 0));
        if (!active)
        {
            Activate();
        }
        //transposer.m_XDamping = 
    }

    private void Activate()
    {
        vcam.Priority = 2;

        active = true;
    }
}
