using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InteractableItemV2 : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public static event HandledNotification OnNoItemFound;
    public delegate void HandledNotification(string notif);

    SOItemData itemData;
    public string itemName;
    bool itemGot;
    [Header("Data or triggering Dialogue")]
    public bool isForCutSceneTrigger;
    public DialogueDatabase dialogueDatabase;
    public int actorID;
    public int conversationID;
    LMActors lmActors;
    void Start()
    {
        lmActors = GameObject.Find("LMActors").GetComponent<LMActors>();
    }
    public void Interact()
    {
        if (!itemGot)
        {
            itemData = Resources.Load<SOItemData>(itemName);
            if (itemData != null)
            {
                OnItemCollected?.Invoke(itemData);
                //OnItemGet?.Invoke(itemData.itemName);
                itemGot = true;
            }
            else
            {
                //No Item Found
                //Debug.Log("No Item Found");
                OnNoItemFound?.Invoke("No Item Found");
            }
            // For Triggering Dialogue
            if (isForCutSceneTrigger)
            {
                if (conversationID > 0)
                {
                    lmActors._LMActors[actorID].GetComponent<DialogueSystemTrigger>().trigger = DialogueSystemTriggerEvent.OnUse;
                    lmActors._LMActors[actorID].GetComponent<DialogueSystemTrigger>().conversation = dialogueDatabase.GetConversation(conversationID).Title;
                }
                lmActors._LMActors[actorID].GetComponent<DialogueSystemTrigger>().OnUse();
            }
        }
        transform.gameObject.tag = "Untagged";
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
