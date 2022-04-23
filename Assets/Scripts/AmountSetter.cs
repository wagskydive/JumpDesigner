using System;
using UnityEngine;

public class AmountSetter : MonoBehaviour
{

    public event Action<int> OnAmountChanged;

    public int CurrentAmount { get; private set; }

    public int increments = 1;

    public int StartAmount = 1;

    public int MaxAmount = 10;

    public int MinAmount = 1;


    private void Start()
    {
        
        CurrentAmount = StartAmount;
        OnAmountChanged?.Invoke(CurrentAmount);
    }


    public void Plus()
    {
        if (CurrentAmount < MaxAmount)
        {
            CurrentAmount += increments;
            OnAmountChanged?.Invoke(CurrentAmount);
        }

    }

    public void Minus()
    {
        if (CurrentAmount > MinAmount)
        {
            CurrentAmount -= increments;
            OnAmountChanged?.Invoke(CurrentAmount);
        }

    }
}
