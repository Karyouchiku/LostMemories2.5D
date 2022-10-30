using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InteractableItem : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public SOItemData itemData;
    bool itemGot;
    [Header("Data or triggering Dialogue")]
    public bool isForCutSceneTrigger;
    public DialogueDatabase dialogueDatabase;
    public DialogueSystemTrigger trigger;
    public int conversationID;
    public void Interact()
    {
        if (!itemGot)
        {
            if (itemData != null)
            {
                OnItemCollected?.Invoke(itemData);
                itemGot = true;
            }
            else
            {
                //No Item Found
            }
            if (isForCutSceneTrigger)
            {
                if (conversationID > 0)
                {
                    trigger.trigger = DialogueSystemTriggerEvent.OnUse;
                    trigger.conversation = dialogueDatabase.GetConversation(8).Title;
                }
                trigger.OnUse();
            }
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            itemGot = this.itemGot
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.itemGot = saveData.itemGot;
    }

    [Serializable]
    struct SaveData
    {
        public bool itemGot;
    }
}
