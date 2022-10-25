using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PixelCrushers.DialogueSystem.Wrappers;

public class ItemFormNPC : MonoBehaviour
{
    public static event HandledItemReceived OnItemReceived;
    public delegate void HandledItemReceived(SOItemData soItemData);

    //public DialogueDatabase dialoguedb;
    SOItemData itemData;
    public string itemName;
    //string item;

    void ItemReceived()
    {
        //item = dialoguedb.GetItem(itemName).Name;
        itemData = Resources.Load<SOItemData>(itemName);
        OnItemReceived?.Invoke(itemData);
    }
}
