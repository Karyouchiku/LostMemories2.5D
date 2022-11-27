using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Notification : MonoBehaviour
{
    public GameObject notificationSection;
    public GameObject itemNotifPrefab;
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
    void ShowNotif(string notifMsg)
    {
        GameObject newItemNotif = Instantiate(itemNotifPrefab);
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
