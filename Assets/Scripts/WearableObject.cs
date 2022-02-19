using System;
using System.Collections.Generic;
using UnityEngine;

public class WearableObject : MonoBehaviour
{
    public static event Action<int> OnWearableEnabled;
    
    [SerializeField]
    public int SkinCoverMaskInt;

    private void Start()
    {
        OnWearableEnabled?.Invoke(SkinCoverMaskInt);
    }

}
