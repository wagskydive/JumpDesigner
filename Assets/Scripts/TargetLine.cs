using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TargetLine : MonoBehaviour
{
    LineRenderer lineRenderer;

    [SerializeField]
    private Transform[] corners;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        if(corners.Length != lineRenderer.numCornerVertices)
        {
            lineRenderer.numCornerVertices = corners.Length;
        }
        for (int i = 0; i < corners.Length; i++)
        {
            lineRenderer.SetPosition(i, corners[i].position);

        }
    }
}
