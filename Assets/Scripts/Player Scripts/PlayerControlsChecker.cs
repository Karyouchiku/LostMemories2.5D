using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsChecker : MonoBehaviour, ISaveable
{
    public PlayerControls player;
    public GameObject ingameUI;
    bool isUIEnabled;
    bool isPlayerControlsEnabled;

    IEnumerator SettingControlsActiveSelf()
    {
        yield return new WaitForFixedUpdate();
        ingameUI.SetActive(isUIEnabled);
        player.enabled = isPlayerControlsEnabled;

    }
    public object SaveState()
    {
        isUIEnabled = ingameUI.activeSelf;
        isPlayerControlsEnabled = player.enabled;
        return new SaveData()
        {
            isUIEnabled = this.isUIEnabled,
            isPlayerControlsEnabled = this.isPlayerControlsEnabled
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        isUIEnabled = saveData.isUIEnabled;
        isPlayerControlsEnabled = saveData.isPlayerControlsEnabled;
        StartCoroutine(SettingControlsActiveSelf());
    }
    [Serializable]
    struct SaveData
    {
        public bool isUIEnabled;
        public bool isPlayerControlsEnabled;
    }
}