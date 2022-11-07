using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteractableDoors : MonoBehaviour, IInteractor, ISaveable
{
    public static event HandlingLockedDoor OnUnlockInteractableDoor;
    public delegate void HandlingLockedDoor(SOItemData key);

    SOItemData key;
    public string keyName;
    
    AnimatedOnTriggerCollider anim;
    PlayerInventory inventory;
    public AudioClip unlockedSfx;
    AudioSource audioSource;
    public bool isUnlocked;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();
        anim = GetComponent<AnimatedOnTriggerCollider>();
    }
    void Update()
    {
        anim.isUnlocked = isUnlocked;
    }

    public void Interact()
    {
        key = Resources.Load<SOItemData>(keyName);
        if (inventory.SearchItemInInventory(key))
        {
            OnUnlockInteractableDoor?.Invoke(key);

            audioSource.clip = unlockedSfx;
            audioSource.Play();
            isUnlocked = true;
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            isUnlocked = this.isUnlocked
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        this.isUnlocked = saveData.isUnlocked;
    }

    [Serializable]
    struct SaveData
    {
        public bool isUnlocked;
    }

}
