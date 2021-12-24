using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider)), RequireComponent(typeof(MovementController))]
public class ColliderHelper : MonoBehaviour
{
    CapsuleCollider collider;

    Transform pelvis;

    MovementController movementController;

    private void Awake()
    {
        collider = GetComponent<CapsuleCollider>();
        movementController = GetComponent<MovementController>();
    }

    void LateUpdate()
    {
        
    }
}
