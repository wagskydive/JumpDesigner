using System.Collections.Generic;
using UnityEngine;

public static class ButtonDecoder
{
    public static bool[] ButtonsDecoded(int CurrentButtonsState)
    {
        List<bool> resultList = new List<bool>();

        int remainder;
        while (CurrentButtonsState > 0)
        {
            remainder = CurrentButtonsState % 2;
            CurrentButtonsState /= 2;
            resultList.Add(remainder > 0);
        }

        return resultList.ToArray();
    }

    public static bool IsButtonPressed(int CurrentButtonsState, int buttonIndex)
    {
        bool[] buttons = ButtonsDecoded(CurrentButtonsState);
        if (buttonIndex >= buttons.Length)
        {
            return false;
        }
        else
        {
            return buttons[buttonIndex];
        }
    }
}
