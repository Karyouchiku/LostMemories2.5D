using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InteractableItem : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);
    
    public static event HandledNotification OnNoItemFound;
    public delegate void HandledNotification(string notif, int type);
    
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
            try
            {
                if (!isForCutSceneTrigger)
                {
                    OnItemCollected?.Invoke(itemData);
                }
                itemGot = true;
                //OnItemGet?.Invoke(itemData.itemName);
            }
            catch
            {
                OnNoItemFound?.Invoke("No Item Found", 3);

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
