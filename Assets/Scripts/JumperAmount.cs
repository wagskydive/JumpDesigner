using System;
using System.Collections.Generic;
using UnityEngine;

public class JumperAmount : MonoBehaviour
{

    public event Action<int> OnAmountChanged;

    public int CurrentAmount { get; private set; }

    private void Start()
    {
        CurrentAmount = 1;
        OnAmountChanged?.Invoke(CurrentAmount);
    }


    public void Plus()
    {
        if (CurrentAmount < 8)
        {
            CurrentAmount++;
            OnAmountChanged?.Invoke(CurrentAmount);
        }
    }

    public void Minus()
    {
        if (CurrentAmount > 1)
        {
            CurrentAmount--;
            OnAmountChanged?.Invoke(CurrentAmount);
        }
    }
}
