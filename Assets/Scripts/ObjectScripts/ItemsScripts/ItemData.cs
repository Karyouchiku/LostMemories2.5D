using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemData
{
    public string soItemDataName;

    public ItemData(Item item)
    {
        soItemDataName = item.soItemData.name;
    }
}
