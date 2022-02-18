using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class JumpAmountText : MonoBehaviour
{
    TMP_Text amountText;

    private void Awake()
    {
        amountText = GetComponent<TMP_Text>();
        GetComponent<JumperAmount>().OnAmountChanged += SetText;
    }

    void SetText(int amount)
    {
        amountText.text = amount.ToString();
    }

}
