using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    //public SOItemData soItemData;
    public string soItemDataName;
    public int stackSize;

    //public InventoryItem(SOItemData soItemData)
    public InventoryItem(string soItemDataName)
    {
        this.soItemDataName = soItemDataName;
        addToStack();
    }

    public void addToStack()
    {
        stackSize++;
    }
    public void removeFromStack()
    {
        stackSize--;
    }

}
