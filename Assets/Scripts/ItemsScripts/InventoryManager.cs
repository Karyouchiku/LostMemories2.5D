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
    public TextMeshProUGUI arrow;
    //public void ShowInventory()
    //{
    //    gameObject.SetActive(true);
    //    OnEnable();
    //}
    //public void CloseInventory()
    //{
    //    gameObject.SetActive(false);
    //}
    bool showInventory;

    [Header("Transition Speed")]
    public float transitionX;
    public float speedDefaultValue = 1000f;
    public float speed;
    public float decreaseSpeed;
    
    Vector3 currentInventoryUILocation;
    
    public void OpenCloseInventory()
    {
        if (showInventory)
        {
            showInventory = false;
        }
        else
        {
            showInventory = true;
        }
    }

    private void Start()
    {
        speed = speedDefaultValue;
    }
    void FixedUpdate()
    {
        if (showInventory)
        {
            if (currentInventoryUILocation.x >= -450f)
            {
                speed -= decreaseSpeed * Time.deltaTime;
                transitionX -= speed * Time.deltaTime;
            }
            else
            {
                speed = speedDefaultValue;
            }
            arrow.text = ">";
        }
        else
        {
            if (currentInventoryUILocation.x < -1)
            {
                speed -= decreaseSpeed * Time.deltaTime;
                transitionX += speed * Time.deltaTime;
            }
            else
            {
                speed = speedDefaultValue;
                currentInventoryUILocation = Vector3.zero;
            }
            arrow.text = "<";
        }
        
        currentInventoryUILocation = new Vector3(transitionX, 0f);
        inventoryUI.anchoredPosition = currentInventoryUILocation;
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
