using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFromNPC : MonoBehaviour
{
    public static event HandledItemReceived OnItemReceived;
    public delegate void HandledItemReceived(SOItemData soItemData);
    
    public static event HandledItemRemoved OnItemRemoved;
    public delegate void HandledItemRemoved(SOItemData soItemData);

    //public static event HandledItemNotification OnItemGetNotif;
    //public delegate void HandledItemNotification(string Notif);

    public SOItemData[] itemData;
    

    public void GiveItem()//Give all in array
    {
        foreach (SOItemData item in itemData)
        {
            OnItemReceived?.Invoke(item);
            //OnItemGetNotif?.Invoke(item.itemName);
        }
    }
    public void GiveItem(int itemID)//give specifically
    {
        OnItemReceived?.Invoke(itemData[itemID]);
        //OnItemGetNotif?.Invoke(itemData[itemID].itemName);
    }

    public void RemoveItem()//remove all in array
    {
        foreach (SOItemData item in itemData)
        {
            OnItemRemoved?.Invoke(item);
        }
    }
    public void RemoveItem(int itemID)//remove specifically
    {
        OnItemRemoved?.Invoke(itemData[itemID]);
    }

}
