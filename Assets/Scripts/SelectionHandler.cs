using System;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField]
    Camera sceneCamera;

    public static event Action<ISelectable> OnSelected;
    public static event Action<ISelectable> OnSelectionConfirmed;
    public static event Action<ISelectable> OnDeselect;

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
        selected = selectable;
        OnSelected?.Invoke(selected);
    }
    public void Deselect()
    {
        if (HasSelection())
        {
            OnDeselect?.Invoke(selected);
            selected = null;
        }
    }

    private void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                if(hit.transform.GetComponent<ISelectable>() != null)
                {
                    if(HasSelection() && hit.transform.GetComponent<ISelectable>() == selected)
                    {
                        ConfirmSelection();
                        
                    }
                    else
                    {
                        hit.transform.GetComponent<ISelectable>().Select();
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
