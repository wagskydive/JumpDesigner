using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyAnimationController : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed = 4f;


}
