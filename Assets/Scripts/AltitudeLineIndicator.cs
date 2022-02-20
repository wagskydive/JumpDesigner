using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AltitudeLineIndicator : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField]
    Transform character;
    [SerializeField]
    GameObject groundIndicator;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        AltitudeOverTerrain.OnTerrainHit += TerrainDetected;
        AltitudeOverTerrain.OnNoHit += NoHit;
    }

    private void NoHit()
    {
        groundIndicator.SetActive(false);
        lineRenderer.enabled = false;
    }

    private void TerrainDetected(Vector3 hitPoint, float distance)
    {
        groundIndicator.SetActive(true);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, character.position);
        lineRenderer.SetPosition(1, hitPoint);
        groundIndicator.transform.position = hitPoint;
    }
}
