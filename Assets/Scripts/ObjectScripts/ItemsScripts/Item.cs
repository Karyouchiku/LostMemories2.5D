using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectible, ISaveable
{
    public static event HandledItemCollected OnItemCollected;
    public delegate void HandledItemCollected(SOItemData soItemData);
    
    
    public SOItemData soItemData;
    
    public AudioSource audioSource;
    public AudioClip clip;

    public bool isNotActive;
    
    void Update()
    {
        if (isNotActive)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    public void Collect()
    {
        OnItemCollected?.Invoke(soItemData);

        audioSource.clip = clip;
        audioSource.Play();
        isNotActive = true;
        Debug.Log($"{soItemData.itemName} is Collected");
    }
    
    public object SaveState()
    {
        return new SaveData()
        {
            isNotActive = this.isNotActive,
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.isNotActive = saveData.isNotActive;
    }

    [Serializable]
    struct SaveData
    {
        public bool isNotActive;
    }
    
}
