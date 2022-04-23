using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;




public class AmountUi : MonoBehaviour
{
    [SerializeField]
    TMP_Text label;
    void Awake()
    {

        GetComponent<AmountSetter>().OnAmountChanged += SetText;
    }

    private void SetText(int obj)
    {
        label.text = obj.ToString();
    }
}
