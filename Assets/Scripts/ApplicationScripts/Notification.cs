using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Notification : MonoBehaviour
{
    public GameObject notificationSection;
    public GameObject itemNotifPrefab;
    public GameObject inGameUI;
    void OnEnable()
    {
        Item.OnItemGet += ShowNotif;
        InteractableItem.OnItemGet += ShowNotif;
        InteractableItemV2.OnItemGet += ShowNotif;
        PlayerInventory.OnItemRemoved += ShowNotif;
    }
    void OnDisable()
    {
        Item.OnItemGet -= ShowNotif;
        InteractableItem.OnItemGet -= ShowNotif;
        InteractableItemV2.OnItemGet -= ShowNotif;
        PlayerInventory.OnItemRemoved -= ShowNotif;
    }
    void ShowNotif(string notifMsg)
    {
        StartCoroutine(inGameUICheck(notifMsg));
    }
    IEnumerator inGameUICheck(string notifMsg)
    {
        if (inGameUI.activeSelf)
        {
            GameObject newItemNotif = Instantiate(itemNotifPrefab);
            newItemNotif.transform.SetParent(notificationSection.transform, false);

            newItemNotif.GetComponent<Animator>().ResetTrigger("Hide");
            newItemNotif.GetComponent<Animator>().SetTrigger("Show");
            newItemNotif.GetComponentInChildren<TMP_Text>().text = notifMsg;
            StartCoroutine(CloseNotif(newItemNotif));
            yield break;

        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            ShowNotif(notifMsg);
        }
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
