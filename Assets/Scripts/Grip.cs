using System;
using UnityEngine;

public class Grip : MonoBehaviour
{
    public static event Action<Grip,Hand> OnSense;
    public static event Action<Grip, Hand> OnUnSense;

    SphereCollider sphereCollider;

    GameObject dockVisual;

    private void Awake()
    {
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.radius = .3f;
        sphereCollider.isTrigger = true;
        dockVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dockVisual.transform.position = transform.position;
        dockVisual.transform.SetParent(transform);
        dockVisual.transform.localScale = Vector3.one * sphereCollider.radius;
        dockVisual.GetComponent<MeshRenderer>().material = MaterialManager.Instance.GreenTransparent;
        dockVisual.SetActive(false);
        //sphereCollider.
    }

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.GetComponent<Hand>();
        if(hand != null)
        {
            hand.GripSense(this);
            OnSense?.Invoke(this, hand);
            dockVisual.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Hand hand = other.GetComponent<Hand>();
        if (hand != null)
        {
            hand.GripUnSense(this);
            dockVisual.SetActive(false);
            OnUnSense?.Invoke(this, hand);
        }

    }
}
