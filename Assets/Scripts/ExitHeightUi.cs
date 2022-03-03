using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ExitHeightUi : MonoBehaviour
{
    TMP_Text label;
    void Awake()
    {
        label = GetComponent<TMP_Text>();
        GetComponent<ExitHeightSetter>().OnHeightChanged += SetText;
    }

    private void SetText(int obj)
    {
        label.text = obj.ToString();
    }
}
