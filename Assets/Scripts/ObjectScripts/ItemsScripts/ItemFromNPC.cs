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
        StartCoroutine(GiveItemDeley());
    }
    IEnumerator GiveItemDeley()
    {
        foreach (SOItemData item in itemData)
        {
            yield return new WaitForSeconds(0.6f);
            OnItemReceived?.Invoke(item);
        }
    }

    public void GiveItem(int itemID)//give specifically
    {
        OnItemReceived?.Invoke(itemData[itemID]);
    }

    public void RemoveItem()//remove all in array
    {
        StartCoroutine(RemoveItemDelay());
    }
    IEnumerator RemoveItemDelay()
    {
        foreach (SOItemData item in itemData)
        {
            yield return new WaitForSeconds(0.6f);
            OnItemRemoved?.Invoke(item);
        }

    }

    public void RemoveItem(int itemID)//remove specifically
    {
        OnItemRemoved?.Invoke(itemData[itemID]);
    }

}
