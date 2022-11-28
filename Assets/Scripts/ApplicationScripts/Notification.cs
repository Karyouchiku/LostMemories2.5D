using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Notification : MonoBehaviour
{
    public GameObject notificationSection;
    public GameObject itemNotifType1;//Added
    public GameObject itemNotifType2;//Removed
    public GameObject itemNotifType3;//No Item Found
    void OnEnable()
    {

        InteractableItem.OnNoItemFound += ShowNotif;
        InteractableItemV2.OnNoItemFound += ShowNotif;

        PlayerInventory.OnItemAddedOrRemoved += ShowNotif;
    }
    void OnDisable()
    {
        InteractableItem.OnNoItemFound -= ShowNotif;
        InteractableItemV2.OnNoItemFound -= ShowNotif;

        PlayerInventory.OnItemAddedOrRemoved -= ShowNotif;

    }
    void ShowNotif(string notifMsg, int type)
    {
        GameObject newItemNotif = null;
        if (type == 1)//Added
        {
            newItemNotif = Instantiate(itemNotifType1);
        }
        else if (type == 2)
        {
            newItemNotif = Instantiate(itemNotifType2);
        }
        else if (type == 3)
        {
            newItemNotif = Instantiate(itemNotifType3);
        }

        newItemNotif.transform.SetParent(notificationSection.transform, false);

        newItemNotif.GetComponent<Animator>().ResetTrigger("Hide");
        newItemNotif.GetComponent<Animator>().SetTrigger("Show");
        newItemNotif.GetComponentInChildren<TMP_Text>().text = notifMsg;
        StartCoroutine(CloseNotif(newItemNotif));
    }
    
    IEnumerator CloseNotif(GameObject newItemNotif)
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(3);
        newItemNotif.GetComponent<Animator>().ResetTrigger("Show");
        newItemNotif.GetComponent<Animator>().SetTrigger("Hide");
        yield return new WaitForSeconds(1);
        Destroy(newItemNotif);
    }
}
