using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using PixelCrushers.DialogueSystem.Wrappers;
using UnityEngine.Events;

public class DialogueModifier : MonoBehaviour, ISaveable
{
    [Header("For Debugging")]
    public bool isDebugging;
    public GameObject DebuggingValues;

    public DialogueDatabase dialoguedb;
    public DialogueDatabase dialoguedbBackup;
    string namePattern = "Burito";

    [Header("Gender Changer")]
    public Animator animator;
    public RuntimeAnimatorController male;
    public RuntimeAnimatorController female;
    void Awake()
    {
        if (PlayerName.playerName == null)
        {
            PlayerName.playerName = "NullName";
        }
        ModifyPlayerNameInDialogues();
        DebuggingValues.SetActive(isDebugging);
    }
    void Start()
    {
        ChangeGender();
    }
    void ChangeGender()
    {
        switch (PlayerName.gender)
        {
            case "Male":
                animator.runtimeAnimatorController = male;
                break;
            case "Female":
                animator.runtimeAnimatorController = female;
                break;
            default:
                animator.runtimeAnimatorController = female;
                Debug.Log("This is the Default Character");
                break;
        }

    }
    public void RestoreDialogues()
    {
        for (int i = 0; i < dialoguedb.conversations.Count; i++)
        {
            for (int j = 0; j < dialoguedb.conversations[i].dialogueEntries.Count; j++)
            {
                if (!isDebugging)
                {
                    dialoguedb.conversations[i].dialogueEntries[j].DialogueText = dialoguedbBackup.conversations[i].dialogueEntries[j].DialogueText;
                }
            }
        }
    }
    
    public void ModifyPlayerNameInDialogues()
    {
        if (!isDebugging)
        {
            dialoguedb.actors[1].Name = PlayerName.playerName;
        }
        for (int i = 0; i < dialoguedb.conversations.Count; i++)
        {
            for (int j = 0; j < dialoguedb.conversations[i].dialogueEntries.Count; j++)
            {
                if (!isDebugging)
                {
                    dialoguedb.conversations[i].dialogueEntries[j].DialogueText = Regex.Replace(dialoguedb.conversations[i].dialogueEntries[j].DialogueText,namePattern, PlayerName.playerName);
                }
            }
        }
    }

    //ADDING LISTENER ON ONCONVERSATIONEND
    UnityAction<Transform> addToOnConversationEnd;
    public GameObject inGameUI;
    public GameObject player;
    public GameObject playerInventory;
    public void AddListenersOnConversationEnd()
    {
        addToOnConversationEnd += EnableIngameUI;
        addToOnConversationEnd += EnablePlayerControls;
        addToOnConversationEnd += InventoryRefresher;
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
        player.GetComponent<PlayerControls>().ResetJoystickValue();
    }
    void InventoryRefresher(Transform playerInventory)
    {
        playerInventory = this.playerInventory.transform;
        playerInventory.GetComponent<PlayerInventory>().InventoryRefresher();
    }

    

    public object SaveState()
    {
        return new SaveData()
        {
            playerName = PlayerName.playerName,
            gender = PlayerName.gender
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        PlayerName.playerName = saveData.playerName;
        PlayerName.gender = saveData.gender;
        ModifyPlayerNameInDialogues();
        ChangeGender();
    }


    [Serializable]
    struct SaveData
    {
        public string playerName;
        public string gender;
    }
}
