using System;
using UnityEngine;
using UnityEngine.UI;

public class AccelAndTouchUiControl : MonoBehaviour, IInput
{

    public Vector4 MovementVector => GetInputs();

    public int CurrentButtonsState => GetCurrentButtonsState();

    [SerializeField]
    private Slider leftSlider;
    DragHelper leftButton;
    
    [SerializeField]
    private Slider rightSlider;
    DragHelper rightButton;

        
    [SerializeField]
    private Slider UpDownSlider;
    DragHelper UpDownButton;

    public void PressButton(int index)
    {
        OnButtonPressed?.Invoke(index);
    }

    private void Awake()
    {
        leftButton = leftSlider.GetComponent<DragHelper>();
        rightButton = rightSlider.GetComponent<DragHelper>();
        UpDownButton = UpDownSlider.GetComponent<DragHelper>();

        leftButton.ÓnDragEndEvent += ResetLeft;
        rightButton.ÓnDragEndEvent += ResetRight;
        UpDownButton.ÓnDragEndEvent += ResetUpDown;


    }

    private void Start()
    {
        SelectionHandler.OnSelectionConfirmed += SetInputs;
        SelectionHandler.OnSelected += Dissapear;

        //SelectionHandler.OnDeselect += RemoveInputs;
    }

    private void Dissapear(ISelectable obj)
    {
        transform.localScale *= 2;
    }

    private void SetInputs(ISelectable obj)
    {
        RemoveInputs();
        currentConnected = obj.transform.GetComponent<MovementController>();
        if (currentConnected != null)
        {
            currentConnected.ReplaceInput(this);
        }
        transform.localScale = Vector3.one;
    }

    MovementController currentConnected;

    public event Action<int> OnButtonPressed;

    private void RemoveInputs()
    {
        if (currentConnected != null)
        {
            currentConnected.ReplaceInput(null);
        }
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

    public int GetCurrentButtonsState()
    {
        int buttonState = 0;
        if (SelectionHandler.Instance.HasSelection())
        {            
            buttonState += 1;
        }
        if (Input.GetMouseButton(1))
        {
            buttonState += 2;
        }
        return buttonState;
    }

}
