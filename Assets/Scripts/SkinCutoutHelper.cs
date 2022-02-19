using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCutoutHelper : MonoBehaviour
{
    Material skinMaterial;

    private void Awake()
    {
        skinMaterial = GetComponent<SkinnedMeshRenderer>().materials[0];
        WearableObject.OnWearableEnabled += SetCutout;
    }

    private void SetCutout(int maskInt)
    {
        skinMaterial.SetFloat("_MaskInt", (float)maskInt);
    }
}
