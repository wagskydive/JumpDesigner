using System;
using UnityEngine;

public class ExitHeightSetter : MonoBehaviour
{

    public event Action<int> OnHeightChanged;

    public int CurrentHeight { get; private set; }


    private void Start()
    {
        
        CurrentHeight = 15000;
        OnHeightChanged?.Invoke(CurrentHeight);
    }


    public void Plus()
    {
        if (CurrentHeight < 22000)
        {
            CurrentHeight += 1000;
            OnHeightChanged?.Invoke(CurrentHeight);
        }
    }

    public void Minus()
    {
        if (CurrentHeight > 1000)
        {
            CurrentHeight -= 1000;
            OnHeightChanged?.Invoke(CurrentHeight);
        }
    }
}
