using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine.Events;

public class DialogueModifier : MonoBehaviour, ISaveable
{
    public DialogueDatabase dialoguedb;
    DialogueDatabase dialoguedbBackup;
    //string thisPlayerName;
    string namePattern = "MC";

    void Awake()
    {
        dialoguedbBackup = dialoguedb;
        //thisPlayerName = PlayerName.playerName;
        if (PlayerName.playerName == null)
        {
            PlayerName.playerName = "NullName";
        }
        ModifyPlayerNameInDialogues();
    }
    public void RestoreDialogues()
    {
        dialoguedb.conversations = dialoguedbBackup.conversations;
        dialoguedb.actors[1].Name = dialoguedbBackup.actors[1].Name;
    }
    
    public void ModifyPlayerNameInDialogues()
    {
        dialoguedb.actors[1].Name = PlayerName.playerName;
        for (int i = 0; i < dialoguedb.conversations.Count; i++)
        {
            for (int j = 0; j < dialoguedb.conversations[i].dialogueEntries.Count; j++)
            {
                dialoguedb.conversations[i].dialogueEntries[j].DialogueText = Regex.Replace(dialoguedb.conversations[i].dialogueEntries[j].DialogueText,namePattern, PlayerName.playerName);
            }
        }
    }

    //ADDING LISTENER ON ONCONVERSATIONEND
    UnityAction<Transform> addToOnConversationEnd;
    public GameObject inGameUI;
    public GameObject player;
    public void AddListenersOnConversationEnd()
    {
        addToOnConversationEnd += EnableIngameUI;
        addToOnConversationEnd += EnablePlayerControls;
        player.GetComponent<DialogueSystemEvents>().conversationEvents.onConversationEnd.AddListener(addToOnConversationEnd);
    }
    void EnableIngameUI(Transform inGameUI)
    {
        inGameUI = this.inGameUI.transform;
        inGameUI.gameObject.SetActive(true);
    }
    void EnablePlayerControls(Transform player)
    {
        player = this.player.transform;
        player.GetComponent<PlayerControls>().enabled = true;
    }

    public object SaveState()
    {
        return new SaveData()
        {
            playerName = PlayerName.playerName
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        PlayerName.playerName = saveData.playerName;
        //thisPlayerName = saveData.playerName;

        //RestoreDialogues();
        ModifyPlayerNameInDialogues();
    }


    [Serializable]
    struct SaveData
    {
        public string playerName;
    }
}
