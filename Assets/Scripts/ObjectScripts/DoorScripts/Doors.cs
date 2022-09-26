using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    
    public PlayerInventory inventory;
    public ItemData Key;
    public bool locked;

    public void Door()
    {
        if (locked)
        {
            LockedDoor();
        }
        else
        {
            UnlockedDoor();
        }
    }
    public void UnlockedDoor()
    {
        Debug.Log("Gettin' Fool");
    }
    public void LockedDoor()
    {
        if (!inventory.SearchItemInInventory(Key))
        {
            Debug.Log("The Door is Lock");
        }
        else
        {
            Debug.Log("Door Unlocked");
            inventory.Remove(Key);
            locked = false;
            //Destroy(gameObject);
        }
    }
}
