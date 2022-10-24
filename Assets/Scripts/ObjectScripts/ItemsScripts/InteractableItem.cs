using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractor
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public SOItemData itemData;
    bool itemGot;
    public void Interact()
    {
        if (!itemGot)
        {
            OnItemCollected?.Invoke(itemData);
            itemGot = true;
        }
    }

}
