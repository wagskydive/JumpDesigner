using UnityEngine;

public class CanopyManualControlHelper : MonoBehaviour
{
    IInput uiInput;

    private void Awake()
    {
        uiInput = FindObjectOfType<AccelAndTouchUiControl>();
        CanopyController canopyController = GetComponent<CanopyController>();
        canopyController.ReplaceInput(uiInput);
    }
}
