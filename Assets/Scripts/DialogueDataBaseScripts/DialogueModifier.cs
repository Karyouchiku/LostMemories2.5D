using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;

public class DialogueModifier : MonoBehaviour, ISaveable
{
    public PlayerName playerName;
    public DialogueDatabase dialoguedb;
    public DialogueDatabase dialoguedbBackup;

    string thisPlayerName;
    string namePattern = "Burito";
    void Start()
    {
        thisPlayerName = playerName.playerName;
        RestorePlayerNameInDialogues();
        ModifyPlayerNameInDialogues();
    }

    public void ModifyPlayerNameInDialogueNames()
    {
        dialoguedb.actors[1].Name = thisPlayerName;
    }

    public void RestorePlayerNameInDialogues()
    {
        dialoguedb.conversations = dialoguedbBackup.conversations;
        
    }
    public void ModifyPlayerNameInDialogues()
    {
        for (int i = 0; i < dialoguedb.conversations.Count; i++)
        {
            for (int j = 0; j < dialoguedb.conversations[i].dialogueEntries.Count; j++)
            {
                dialoguedb.conversations[i].dialogueEntries[j].DialogueText = Regex.Replace(dialoguedb.conversations[i].dialogueEntries[j].DialogueText,namePattern, thisPlayerName);
            }
        }
    }


    public object SaveState()
    {
        return new SaveData()
        {
            playerName = thisPlayerName
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        playerName.playerName = saveData.playerName;
        thisPlayerName = saveData.playerName;
        RestorePlayerNameInDialogues();
        ModifyPlayerNameInDialogueNames();
        ModifyPlayerNameInDialogues();
    }


    [Serializable]
    struct SaveData
    {
        public string playerName;
    }
}
