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

    public bool isActive;
    
    void FixedUpdate()
    {
        GetComponent<CapsuleCollider>().enabled = isActive;
        GetComponentInChildren<SpriteRenderer>().enabled = isActive;
    }

    public void Collect()
    {
        OnItemCollected?.Invoke(soItemData);

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
        this.isActive = saveData.isActive;
    }

    [Serializable]
    struct SaveData
    {
        public bool isActive;
    }
    
}
