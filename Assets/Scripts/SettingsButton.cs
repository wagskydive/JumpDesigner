using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel;

    public void SettingsButtonPress()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
}
