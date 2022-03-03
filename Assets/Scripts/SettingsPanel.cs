using System;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnPlayJump;

    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;

    public void PlayButtonPress()
    {

    }
}
