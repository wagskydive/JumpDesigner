using UnityEngine;
using UnityEngine.EventSystems;


public class Selectable : MonoBehaviour, ISelectable
{

    public void Select()
    {
        SelectionHandler.Instance.SetSelection(this);
    }
}