using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, ISaveable
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> inventory = new List<InventoryItem>();
    Dictionary<string, InventoryItem> itemDictionary = new Dictionary<string, InventoryItem>();
    
    public static event HandledNotification OnItemAddedOrRemoved;
    public delegate void HandledNotification(string notif);

    public GameObject inventoryPanel;

    //public InventoryManager inventoryManager;
    void Awake()
    {
        InventoryRefresher();
    }

    public void InventoryRefresher()
    {
        //inventoryManager.Refresher(inventory);
        OnInventoryChange?.Invoke(inventory);
    }
    void OnEnable()
    {
        Item.OnItemCollected += Add;
        InteractableItem.OnItemCollected += Add;
        InteractableItemV2.OnItemCollected += Add;
        ItemFromNPC.OnItemReceived += Add;

        ItemFromNPC.OnItemRemoved += Remove;
        InteractableDoor.RemoveFromInv += Remove;
        PortalDoor.RemoveFromInv += Remove;
        LockInteractableDoors.OnUnlockInteractableDoor += Remove;
    }
    void OnDisable()
    {
        Item.OnItemCollected -= Add;
        InteractableItem.OnItemCollected -= Add;
        InteractableItemV2.OnItemCollected -= Add;
        ItemFromNPC.OnItemReceived -= Add;

        ItemFromNPC.OnItemRemoved -= Remove;
        InteractableDoor.RemoveFromInv -= Remove;
        PortalDoor.RemoveFromInv -= Remove;
        LockInteractableDoors.OnUnlockInteractableDoor -= Remove;
    }

    public void Add(SOItemData soItemData)
    {
        if (itemDictionary.TryGetValue(soItemData.name, out InventoryItem item))
        {
            item.addToStack();
            Debug.Log($"{soItemData.itemName} Total stack now {item.stackSize}");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(soItemData.name);
            inventory.Add(newItem);
            itemDictionary.Add(soItemData.name, newItem);
        }
        OnInventoryChange?.Invoke(inventory);
        OnItemAddedOrRemoved?.Invoke(soItemData.itemName);
    }

    public void Remove(SOItemData soItemData)
    {
        if (itemDictionary.TryGetValue(soItemData.name, out InventoryItem item))
        {
            item.removeFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(soItemData.name);
                OnInventoryChange?.Invoke(inventory);
                OnItemAddedOrRemoved?.Invoke($"Removed: {soItemData.itemName}");
            }
        }
    }
    public bool SearchItemInInventory(SOItemData soItemData)
    {
        if (itemDictionary.TryGetValue(soItemData.name, out InventoryItem item))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    //SAVE THE EXISTING DATA FROM INVENTORY FUCKER
    public object SaveState()
    {
        return new SaveData()
        {
            inventory = this.inventory,
            itemDictionary = this.itemDictionary
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.inventory = saveData.inventory;
        this.itemDictionary = saveData.itemDictionary;
        StartCoroutine(RefreshAfterFixedFrameUpdate());
        

    }
    IEnumerator RefreshAfterFixedFrameUpdate()
    {
        yield return new WaitForFixedUpdate();
        InventoryRefresher();
    }
    [Serializable]
    struct SaveData
    {
        public List<InventoryItem> inventory;
        public Dictionary<string, InventoryItem> itemDictionary;
    }
    
}
