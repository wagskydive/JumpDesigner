using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanopyAnimationHandler : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    CanopyController canopyController;

    private void Start()
    {
        canopyController = GetComponent<CanopyController>();
        canopyController.OnPull += HandlePull;
    }

    private void HandlePull()
    {
        animator.SetBool("Pull", true);
    }
}
