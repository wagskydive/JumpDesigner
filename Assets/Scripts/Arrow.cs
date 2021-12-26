using UnityEngine;

public class Arrow : MonoBehaviour
{
    SkydiveManager skydiveManager;
    [SerializeField]
    GameObject arrow;

    Transform origin;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
        //SelectionHandler.OnSelected += HandleSelected;
        SelectionHandler.OnTakeControlConfirmed += HandleSelectedConfirmed;
        SelectionHandler.OnDeselected += HandleDeselected;
        arrow.SetActive(false);

    }

    private void HandleSelectedConfirmed(ISelectable obj)
    {
        origin = obj.transform;
        target = skydiveManager.middlepointNPCS;
        arrow.SetActive(true);
    }


    private void HandleDeselected(ISelectable obj)
    {
        //arrow.SetActive(false);
    }
    private void Update()
    {
        if(transform != null && origin != null)
        {
            transform.position = origin.position;
            transform.LookAt(target);
        }

    }

}
