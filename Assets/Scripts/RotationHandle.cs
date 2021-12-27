using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationHandle : MonoBehaviour
{
    [SerializeField]
    SelectedInput selectedInput;

    private void OnMouseDown()
    {
        selectedInput.SetRotating(true);
        Debug.Log("Rotations start");
    }
    private void OnMouseUp()
    {
        //selectedInput.SetRotating(false);
        //Debug.Log("Rotations end");
    }
    
}
