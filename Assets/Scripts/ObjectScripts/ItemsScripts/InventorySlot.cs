using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text itemName;
    public Text stackSizeText;

    public void ClearSlot()
    {
        icon.enabled = false;
        itemName.enabled = false;
        stackSizeText.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        itemName.enabled = true;

        icon.sprite = GetSOItemData(item.soItemDataName).icon;
        icon.color = new Color(255, 255, 255, 255);

        itemName.text = GetSOItemData(item.soItemDataName).itemName;
        if (item.stackSize > 1)
        {
            stackSizeText.enabled = true;
            stackSizeText.text = item.stackSize.ToString();
        }
    }

    SOItemData GetSOItemData(string soItemDataname)
    {
        SOItemData soItemData = Resources.Load<SOItemData>(soItemDataname);
        return soItemData;
    }

}
