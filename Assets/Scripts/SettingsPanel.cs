using System;
using UnityEngine;

public class SettingsPanel : MonoBehaviour
{
    public event Action OnPlayJump;

    [SerializeField]
    JumpSequenceSelector jumpSequenceSelector;

    [SerializeField]
    ExitHeightSetter heightSetter;

    SkydiveManager skydiveManager;

    private void Awake()
    {
        skydiveManager = FindObjectOfType<SkydiveManager>();
    }

    public void PlayButtonPress()
    {
        float altitude = heightSetter.CurrentHeight / 3.28f;
        skydiveManager.SetupJumpRun(jumpSequenceSelector.SelectedSequence,Mathf.RoundToInt(altitude));
        transform.root.gameObject.SetActive(false);
    }
}
