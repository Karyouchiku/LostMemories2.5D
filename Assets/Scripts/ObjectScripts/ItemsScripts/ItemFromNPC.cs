using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFromNPC : MonoBehaviour
{
    public static event HandledItemReceived OnItemReceived;
    public delegate void HandledItemReceived(SOItemData soItemData);

    //public DialogueDatabase dialoguedb;
    public SOItemData[] itemData;
    //public string itemName;
    //string item;

    public void GiveItem()
    {
        //item = dialoguedb.GetItem(itemName).Name;
        //itemData = Resources.Load<SOItemData>(itemName);
        for (int i = 0; i < itemData.Length; i++)
        {
            OnItemReceived?.Invoke(itemData[i]);
        }
    }
}
