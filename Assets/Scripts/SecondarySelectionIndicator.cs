using System;
using UnityEngine;

public class SecondarySelectionIndicator : MonoBehaviour
{
    

    GameObject[] slotIndicators;

    void SetAllActive(bool value)
    {
        for (int i = 0; i < slotIndicators.Length; i++)
        {
            slotIndicators[i].SetActive(value);
        }
    }

    ISelectable secondayeSelected;
    private void Start()
    {
        SelectionHandler.OnSecondarySelected += AttachSelected;
        SelectionHandler.OnDeselected += DetachSelected;
    }

    private void DetachSelected(ISelectable obj)
    {
        SetAllActive(false);
    }

    private void Awake()
    {
        slotIndicators = new GameObject[8];


        slotIndicators[0] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[0].transform.localPosition = new Vector3(-1, 0, 1);
        slotIndicators[0].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[1] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[1].transform.localPosition = new Vector3(0, 0, 1);
        slotIndicators[1].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;


        slotIndicators[2] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[2].transform.localPosition = new Vector3(1, 0, 1);
        slotIndicators[2].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[3] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[3].transform.localPosition = new Vector3(-1, 0, 0);
        slotIndicators[3].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[4] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[4].transform.localPosition = new Vector3(1, 0, 0);
        slotIndicators[4].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[5] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[5].transform.localPosition = new Vector3(-1, 0, -1);
        slotIndicators[5].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[6] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[6].transform.localPosition = new Vector3(0, 0, -1);
        slotIndicators[6].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        slotIndicators[7] = Instantiate(Resources.Load("Prefabs/FormationSlotIndicator") as GameObject, transform);
        slotIndicators[7].transform.localPosition = new Vector3(1, 0, -1);
        slotIndicators[7].GetComponent<FormationSlotIndicator>().OnClicked += HandleClicked;

        SetAllActive(false);
    }

    private void HandleClicked(FormationSlotIndicator obj)
    {
        for (int i = 0; i < slotIndicators.Length; i++)
        {
            if(obj == slotIndicators[i].GetComponent<FormationSlotIndicator>())
            {
                SelectionHandler.Instance.SetSlotTarget(secondayeSelected, i + 1,0);
            }
        }
        
    }

    private void AttachSelected(ISelectable obj)
    {

        secondayeSelected = obj;
        transform.position = secondayeSelected.transform.position;
        SetAllActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (secondayeSelected != null)
        {
            transform.position = secondayeSelected.transform.position;
            transform.rotation = secondayeSelected.transform.rotation;
        }
    }

}
