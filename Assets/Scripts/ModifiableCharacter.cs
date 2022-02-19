using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ModifiableCharacter : MonoBehaviour
{
    Animator animator;

    Avatar male;
    Avatar female;

    int clothingIndex;

    int clothingColor;

    float skinColor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 20);
    }
}
