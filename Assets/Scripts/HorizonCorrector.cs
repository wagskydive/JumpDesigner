using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class HorizonCorrector : MonoBehaviour
{
    [SerializeField]
    float dutchMultiplier;

    [SerializeField]
    int average = 5;

    float[] lastInputs;


    int currentAveragInt = 0;


    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        lastInputs = new float[average];
        for (int i = 0; i < average; i++)
        {
            lastInputs[i] = Input.acceleration.x;
        }


    }

    // Update is called once per frame
    void Update()
    {

        lastInputs[currentAveragInt] = Input.acceleration.x;

        currentAveragInt++;
        if (currentAveragInt > average - 1)
        {
            currentAveragInt = 0;
        }



        vcam.m_Lens.Dutch = GetAverage() * dutchMultiplier;
    }

    float GetAverage()
    {
        float run = 0;
        for (int i = 0; i < average; i++)
        {
            run += lastInputs[i];
        }
        return run / average;
    }

}
