using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiDigitalSpeedMeter : MonoBehaviour
{
    TMP_Text speedText;

    [SerializeField]
    Transform character;
    [SerializeField]
    Transform terrain;

    float lastYPos;
    float[] speedBuffer;
    int index = 0;
    int bufferSize = 15;
    private void Start()
    {
        speedText = GetComponent<TMP_Text>();
        speedBuffer = new float[bufferSize];
    }

    private void Update()
    {
        Debug.Log((Mathf.Round((character.position.y - lastYPos) / Time.deltaTime) * -1));
        
        speedBuffer[index] = ((character.position.y - lastYPos) / Time.deltaTime) * -1;

        speedText.text = Mathf.Round(AverageSpeed()).ToString();
        index++;
        if(index >= bufferSize)
        {
            index = 0;
        }
        lastYPos = character.position.y;
    }
    float AverageSpeed()
    {
        float runningTotal = 0;
        for (int i = 0; i < speedBuffer.Length; i++)
        {
            runningTotal += speedBuffer[i];
        }
        return runningTotal / speedBuffer.Length;
    }

}
