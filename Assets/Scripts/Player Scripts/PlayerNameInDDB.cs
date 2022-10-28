using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;

public class PlayerNameInDDB : MonoBehaviour, ISaveable
{
    public PlayerName playerName;
    public DialogueDatabase dialoguedb;

    string thisPlayername;
    
    void FixedUpdate()
    {
        thisPlayername = playerName.playerName;
        dialoguedb.actors[1].Name = thisPlayername;
    }
    public object SaveState()
    {
        return new SaveData()
        {
            playerName = thisPlayername

        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        thisPlayername = saveData.playerName;
        playerName.playerName = saveData.playerName;
    }

    
    [Serializable]
    struct SaveData
    {
        public string playerName;
    }

}
