using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicator : MonoBehaviour
{
    [SerializeField]
    GameObject innerIndicator;

    ISelectable selected;

    void Start()
    {
        SelectionHandler.OnSelected += AttachSelected;
        SelectionHandler.OnDeselected += DetachSelected;
        innerIndicator.SetActive(false);
    }

    private void DetachSelected(ISelectable obj)
    {
        selected = null;
        innerIndicator.SetActive(false);
    }

    private void AttachSelected(ISelectable obj)
    {
        innerIndicator.SetActive(true);
        selected = obj;
        transform.position = selected.transform.position;
        innerIndicator.transform.localScale = Vector3.one * 2;
        //LeanTween.rotateAround(innerIndicator, Vector3.up, 270, .9f).setEaseOutSine();
        LeanTween.scale(innerIndicator, Vector3.one, .5f).setEaseOutElastic();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected != null)
        {
            transform.position = selected.transform.position;
        }
    }
}
