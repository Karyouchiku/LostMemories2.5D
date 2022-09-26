using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(ItemData itemData);
    public ItemData itemData;
    

    public void Collect()
    {
        Debug.Log($"{itemData.displayName} is Collected");
        Destroy(gameObject);
        OnItemCollected?.Invoke(itemData);
    }

}