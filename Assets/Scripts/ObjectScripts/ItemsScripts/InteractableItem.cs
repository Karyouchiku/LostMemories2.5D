using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractor
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public SOItemData itemData;
    public void Interact()
    {
        OnItemCollected?.Invoke(itemData);
        Debug.Log($"Got Some Food: {itemData.itemName}");
    }

}
