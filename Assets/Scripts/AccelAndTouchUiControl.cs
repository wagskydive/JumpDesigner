using System;
using UnityEngine;
using UnityEngine.UI;

public class AccelAndTouchUiControl : MonoBehaviour, IInput
{

    public Vector4 MovementVector => GetInputs();

    [SerializeField]
    private Slider leftSlider;
    DragHelper leftButton;
    
    [SerializeField]
    private Slider rightSlider;
    DragHelper rightButton;

        
    [SerializeField]
    private Slider UpDownSlider;
    DragHelper UpDownButton;



    private void Awake()
    {
        leftButton = leftSlider.GetComponent<DragHelper>();
        rightButton = rightSlider.GetComponent<DragHelper>();
        UpDownButton = UpDownSlider.GetComponent<DragHelper>();

        leftButton.ÓnDragEndEvent += ResetLeft;
        rightButton.ÓnDragEndEvent += ResetRight;
        UpDownButton.ÓnDragEndEvent += ResetUpDown;


    }

    private void ResetUpDown()
    {
        ResetSlider(UpDownSlider);
    }

    private void ResetRight()
    {
        ResetSlider(rightSlider);
    }

    private void ResetLeft()
    {
        ResetSlider(leftSlider);
    }

    private void ResetSlider(Slider slider)
    {
        slider.value = 0;
    }

    private Vector4 GetInputs()
    {
        
        return (new Vector4(rightSlider.value, UpDownSlider.value, leftSlider.value, Input.acceleration.x));
        
    }

    public int CurrentButtonsState => throw new System.NotImplementedException();
}
