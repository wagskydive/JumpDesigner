using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedInput : MonoBehaviour, IInput
{
    public Vector4 MovementVector => GetMovementVector();

    private void Start()
    {
        //SelectionHandler.OnSelected += SetInputs;
        //SelectionHandler.OnDeselected += UnSetInputs;
    }

    private void UnSetInputs(ISelectable obj)
    {
        if(obj.transform.GetComponent<MovementController>().inputSource == this)
        {
            obj.transform.GetComponent<MovementController>().ReplaceInput(null);
        }
        
    }

    private void SetInputs(ISelectable obj)
    {
        obj.transform.GetComponent<MovementController>().ReplaceInput(this);
    }

    public void SetRotating(bool value)
    {
        isRotating = value;
        lastMousePosition = Input.mousePosition;
    }

    bool isRotating;

    Vector3 lastMousePosition;

    private Vector4 GetMovementVector()
    {
        Vector4 inputs = Vector4.zero;
        if (isRotating)
        {
            //inputs.w = -Mathf.Clamp((Input.mousePosition.x - lastMousePosition.x) / 30,-1,1);
            //if (Input.GetMouseButtonUp(0))
            //{
            //    isRotating = false;
            //}
        }


        lastMousePosition = Input.mousePosition;
        return inputs;
    }

    public int CurrentButtonsState => 0;

    public event Action<int> OnButtonPressed;
}
