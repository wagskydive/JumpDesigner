using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMenu : MonoBehaviour
{
    ISelectable currentSelected;
    List<ISelectable> currentSelectedList = new List<ISelectable>();

    [SerializeField]
    GameObject transitionsMenu;

    [SerializeField]
    GameObject takeControlButtonMenu;

    private void Awake()
    {
        SelectionHandler.OnSelected += SetSelected;
        SelectionHandler.OnSelectedList += SetSelectedList;
        SelectionHandler.OnDeselected += SetDeselected;
        transitionsMenu.SetActive(false);
    }

    private void SetDeselected(ISelectable obj)
    {
 
        currentSelected = null;
        LeanTween.scale(transitionsMenu, Vector3.zero, .4f).setEaseOutExpo().setOnComplete(SetMenuInactive);
        LeanTween.scale(takeControlButtonMenu, Vector3.zero, .4f).setEaseOutExpo().setOnComplete(SetControlButtonInactive);

        
        
    }

    void SetMenuInactive()
    {
        transitionsMenu.SetActive(false);
    }


    void SetControlButtonInactive()
    {
        takeControlButtonMenu.SetActive(false);
    }


    private void SetSelected(ISelectable obj)
    {
        currentSelected = obj;


        transitionsMenu.SetActive(true);
        LeanTween.scale(transitionsMenu, Vector3.one, .4f).setEaseOutElastic();
        takeControlButtonMenu.SetActive(true);
        LeanTween.scale(takeControlButtonMenu, Vector3.one, .4f).setEaseOutElastic();

    }
    
    private void SetSelectedList(List<ISelectable> obj)
    {
        currentSelectedList = obj;
        transitionsMenu.SetActive(true);
        LeanTween.scale(transitionsMenu, Vector3.one, .4f).setEaseOutElastic();
    }



    public void ForwardButtonPress()
    {
        if(currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionForward();
        }
        if(currentSelectedList.Count > 0)
        {
            for (int i = 0; i < currentSelectedList.Count; i++)
            {
                currentSelectedList[i].transform.GetComponent<MovementController>().TransitionForward();
            }
        }
        
    }


    public void BackwardButtonPress()
    {
        if (currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionBackward();
        }
        if (currentSelectedList.Count > 0)
        {
            for (int i = 0; i < currentSelectedList.Count; i++)
            {
                currentSelectedList[i].transform.GetComponent<MovementController>().TransitionBackward();
            }
        }
    }
    public void SideButtonPress()
    {
        if (currentSelected != null)
        {
            currentSelected.transform.GetComponent<MovementController>().TransitionLeft();
        }
        if (currentSelectedList.Count > 0)
        {
            for (int i = 0; i < currentSelectedList.Count; i++)
            {
                currentSelectedList[i].transform.GetComponent<MovementController>().TransitionLeft();
            }
        }
    }
}
