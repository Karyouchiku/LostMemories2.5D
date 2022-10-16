using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>(4);
    public RectTransform inventoryUI;
    
    //For Testing
    //Show Inventory Arrow
    public TextMeshProUGUI arrowButton;
    
    bool showInventory;

    [Header("Inventory Animation")]
    public Animator show_HideInventory;

    //ONClick Button
    public void OpenCloseInventory()
    {
        if (showInventory)
        {
            showInventory = false;
            arrowButton.text = "<";
        }
        else
        {
            showInventory = true;
            arrowButton.text = ">";
        }
        show_HideInventory.SetBool("show_HideInventory", showInventory);
    }
    public void Refresher(List<InventoryItem> inventory)
    {
        DrawInventory(inventory);
    }

    void OnEnable()
    {
        PlayerInventory.OnInventoryChange += DrawInventory;
    }
    void OnDisable()
    {
        PlayerInventory.OnInventoryChange -= DrawInventory;
    }
    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(4);
    }
    void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for (int i = 0; i < inventorySlots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);
    }
}
