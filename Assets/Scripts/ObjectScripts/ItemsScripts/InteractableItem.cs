using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public SOItemData itemData;
    bool itemGot;
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
                Debug.Log("No fucking item here bro");
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
