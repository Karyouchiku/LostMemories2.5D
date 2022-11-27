using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class InteractableItemV3 : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public SOItemData[] itemData;
    bool itemGot;
    public PortalDoorV2 requiredItems;
    public void Interact()
    {
        IObjectives.SetObjective1();
        if (!itemGot)
        {
            StartCoroutine(StoreDelay());
        }
        requiredItems.gotAllItems = true;
        itemGot = true;
        transform.gameObject.tag = "Untagged";
    }
    IEnumerator StoreDelay()
    {
        foreach (SOItemData item in itemData)
        {
            yield return new WaitForSeconds(0.4f);
            OnItemCollected?.Invoke(item);
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
