using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField]
    Camera sceneCamera;

    public static event Action<ISelectable> OnSelected;
    public static event Action<ISelectable> OnSelectionConfirmed;
    public static event Action<ISelectable> OnDeselected;

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
       if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject(0))
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
                        SetSelection(selectedHit);
                    }
                    else if (selectedHit == selected)
                    {
                        ConfirmSelection();
                        Deselect();
                    }
                    else
                    {
                        MoveToCommand(selectedHit);
                    }

                    
                    
                }
                else if (HasSelection())
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

    private void ConfirmSelection()
    {
        if (HasSelection())
        {
            OnSelectionConfirmed?.Invoke(selected);
        }
        
    }
}
