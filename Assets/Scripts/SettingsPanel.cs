using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnPlayJump;

    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;

    [SerializeField]
    ExitHeightSetter heightSetter;

    SkydiveManager skydiveManager;

    [SerializeField]
    Toggle infintyToggle;
    [SerializeField]
    GameObject terrain;

    private void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
    }

    public void PlayButtonPress()
    {
        float altitude = heightSetter.CurrentHeight / 3.28f;
        skydiveManager.SetupJumpRun(jumpSequenceSelector.SelectedSequence,Mathf.RoundToInt(altitude));
        transform.root.gameObject.SetActive(false);
        if (infintyToggle.isOn)
        {
            terrain.SetActive(false);
        }
        else
        {
            terrain.SetActive(true);
        }
    }
}
