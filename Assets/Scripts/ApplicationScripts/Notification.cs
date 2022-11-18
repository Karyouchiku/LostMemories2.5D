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
        Item.OnItemGet += ShowNotif;
        InteractableItem.OnItemGet += ShowNotif;
        InteractableItemV2.OnItemGet += ShowNotif;
    }
    void OnDisable()
    {
        Item.OnItemGet -= ShowNotif;
        InteractableItem.OnItemGet -= ShowNotif;
        InteractableItemV2.OnItemGet -= ShowNotif;
    }
    void ShowNotif(string notifMsg)
    {
        GameObject newItemNotif = Instantiate(itemNotifPrefab);
        newItemNotif.transform.SetParent(notificationSection.transform, false);

        //notifList.Add(newItemNotif);

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
