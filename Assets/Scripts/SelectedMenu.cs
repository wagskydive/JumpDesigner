using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMenu : MonoBehaviour
{
    ISelectable currentSelected;

    [SerializeField]
    GameObject transitionsMenu;

    private void Awake()
    {
        SelectionHandler.OnSelected += SetSelected;
        SelectionHandler.OnDeselected += SetDeselected;
        transitionsMenu.SetActive(false);
    }

    private void SetDeselected(ISelectable obj)
    {
        currentSelected = null;
        transitionsMenu.SetActive(false);
    }

    private void SetSelected(ISelectable obj)
    {
        currentSelected = obj;
        transitionsMenu.SetActive(true);
    }

    public void ForwardButtonPress()
    {
        if(currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionForward();
        }
        
    }
    public void BackwardButtonPress()
    {
        if (currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionBackward();
        }
    }
    public void SideButtonPress()
    {
        if (currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionLeft();
        }
    }
}
