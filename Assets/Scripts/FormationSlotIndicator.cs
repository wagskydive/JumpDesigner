using System;
using System.Collections.Generic;
using UnityEngine;

public class FormationSlotIndicator : MonoBehaviour
{
    public event Action<FormationSlotIndicator> OnClicked;

    [SerializeField]
    GameObject visual;

    ISelectable _selectable;

    private void Start()
    {
        visual.transform.localScale = Vector3.one * .5f;
    }

    public void SetSelected(ISelectable selectable)
    {
        _selectable = selectable;
    }

    private void OnMouseDown()
    {
        OnClicked?.Invoke(this);
        LeanTween.scale(visual, Vector3.one, .2f).setEaseInOutExpo();
        
    }

    private void OnMouseUp()
    {        
        LeanTween.scale(visual, Vector3.one * .5f, .2f).setEaseInOutExpo();
    }


}
