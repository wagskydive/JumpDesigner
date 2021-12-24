using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputButton : MonoBehaviour, IPointerClickHandler 
{

    [SerializeField]
    public int buttonIndex;

    [SerializeField]
    public AccelAndTouchUiControl uiControl;

    public void OnPointerClick(PointerEventData eventData)
    {
        uiControl.PressButton(buttonIndex);
    }


}
