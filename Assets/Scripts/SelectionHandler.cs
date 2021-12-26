﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField]
    Camera sceneCamera;

    public static event Action<ISelectable> OnSelected;
    public static event Action<List<ISelectable>> OnSelectedList;
    public static event Action<ISelectable> OnTakeControlConfirmed;
    public static event Action<ISelectable> OnDeselected;
    public static event Action<ISelectable,Vector3> OnDrag;

    public static SelectionHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SelectionHandler>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned Selection Manager", typeof(SelectionHandler)).GetComponent<SelectionHandler>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static SelectionHandler instance;

    [SerializeField]
    public ISelectable selected;


    [SerializeField]
    public ISelectable player;


    [SerializeField]
    public List<ISelectable> selectedList = new List<ISelectable>();

    public bool HasSelection()
    {
        return selected != null;
    }

    public ISelectable GetSelected()
    {
        if (HasSelection())
        {
            return selected;
        }
        else
        {
            return null;
        }
    }

    public void SelectFirst()
    {
        SkydiveManager skydiveManager = FindObjectOfType<SkydiveManager>();
        if (skydiveManager.SpawnedSkydivers.Count > 0)
        {
            SetSelection(skydiveManager.SpawnedSkydivers[0]);
        }
    }

    public void SetSelection(ISelectable selectable)
    {
        selectable.Select();
        selected = selectable;
        
        OnSelected?.Invoke(selected);
    }
    public void Deselect()
    {
        if (HasSelection())
        {
            selected.Deselect();
            OnDeselected?.Invoke(selected);
            selected = null;
        }
        if(selectedList.Count > 0)
        {
            selectedList = new List<ISelectable>();
        }
    }

    public void SetSelectionList(List<ISelectable> selectable)
    {

        selectedList = selectable;

        OnSelectedList?.Invoke(selectedList);
    }


    void MoveToCommand(ISelectable target)
    {
        if(selected != null && selected.GetType() == typeof(NPC_Ai_FromState))
        {
            NPC_Ai_FromState selecterAi = (NPC_Ai_FromState)selected;
            selecterAi.SetState(new SkydiveState(target));
        }
    }

    private void Update()
    {
       if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(0))
        {
            
            RaycastHit hit;
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {


                ISelectable selectedHit = hit.transform.GetComponent<ISelectable>();
                if (selectedHit != null)
                {
                    if(!HasSelection())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            SetSelection(selectedHit);
                        }
                        
                    }
                    else if (selectedHit == selected)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            //ResetDrag();
                        }
                        Drag(hit.point);
                            
                    }
                    else
                    {
                        SetSelection(selectedHit);
                    }

                    
                    
                }
                else if (HasSelection() && Input.GetMouseButtonDown(0))
                {
                    Deselect();
                }

            }
            else if (HasSelection())
            {
                Deselect();
            }
        }
    }

    //private void ResetDrag()
    //{
    //    lastMousePosition = Input.mousePosition;
    //}
    //
    //Vector3 lastPositionCharacter;
    //Vector3 lastMousePosition;

    

    private void Drag(Vector3 hit)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = sceneCamera.WorldToScreenPoint(hit).z;
        //
        //
        //
        //Vector3 MouseWorldPosition = sceneCamera.ScreenToWorldPoint(mousePosition);
        //
        //Vector3 mouseVelocity = MouseWorldPosition-lastMousePosition;
        //
        //Vector3 hitDiff = hit - selected.transform.position;
        //
        //
        //selected.transform.position += hitDiff.Flatten();

        //lastMousePosition = MouseWorldPosition;
        //OnDrag?.Invoke(selected, mouseVelocity);

    }

    public void TakeControOfSelection()
    {
        if (HasSelection())
        {
            player = selected;
            OnTakeControlConfirmed?.Invoke(selected);
            Deselect();
        }
        
    }
}
