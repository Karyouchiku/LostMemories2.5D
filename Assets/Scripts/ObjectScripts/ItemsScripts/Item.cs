using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);

    public static event HandledNotification OnItemGet;
    public delegate void HandledNotification(string notif);

    public SOItemData soItemData;
    
    AudioSource audioSource;
    public AudioClip clip;

    public bool isActive;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = soItemData.icon;
        audioSource = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        GetComponent<CapsuleCollider>().enabled = isActive;
        GetComponentInChildren<SpriteRenderer>().enabled = isActive;
    }

    public void Collect()
    {
        OnItemCollected?.Invoke(soItemData);
        OnItemGet?.Invoke(soItemData.itemName);
        audioSource.clip = clip;
        audioSource.Play();
        isActive = false;
        Debug.Log($"{soItemData.itemName} is Collected");
    }
    
    public object SaveState()
    {
        return new SaveData()
        {
            isActive = this.isActive,
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        StartCoroutine(SetItemActiveState(saveData.isActive));
    }
    IEnumerator SetItemActiveState(bool isActive)
    {
        yield return new WaitForFixedUpdate();
        this.isActive = isActive;
    }

    [Serializable]
    struct SaveData
    {
        public bool isActive;
    }
    
}
