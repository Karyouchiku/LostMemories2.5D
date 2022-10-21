using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, ISaveable
{
    PlayerInventory inventory;
    [Header("Door Info")]
    public SOItemData key;
    public bool locked;

    [Header("Audio Clips")]
    public AudioClip unlockedWithKey;
    public AudioClip doorOpen;
    public AudioClip doorKnocking;


    AudioSource audioSource;

    [Header("Gate Properties")]
    public Animator gate1;
    public Animator gate2;
    public bool isOpen;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventory>();
        audioSource = GameObject.Find("OtherSFX").GetComponent<AudioSource>();
    }

    public void OpenGate()
    {
        if (locked)
        {
            LockedDoor();
        }
        else
        {
            UnlockedDoor();
        }
    }
    void UnlockedDoor()
    {
        isOpen = !isOpen;
        playAudio(doorOpen, 0.7f);
        gate1.SetBool("isOpen", isOpen);
        gate2.SetBool("isOpen", isOpen);
    }

    void LockedDoor()
    {
        if (!inventory.SearchItemInInventory(key))
        {

            playAudio(doorKnocking, 0.4f);
            Debug.Log("The Door is Lock");
        }
        else
        {
            Debug.Log("Door Unlocked");
            inventory.Remove(key);
            locked = false;
            playAudio(unlockedWithKey, 0.5f);
            
        }

    }
    //For Playing SFX
    void playAudio(AudioClip clip, float vol)
    {
        try
        {
            audioSource.volume = vol;
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
        catch (Exception)
        {
            Debug.Log("No SFX");
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            locked = this.locked,
            isOpen = this.isOpen
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.locked = saveData.locked;
        this.isOpen = saveData.isOpen;
    }
    [Serializable]
    struct SaveData
    {
        public bool locked;
        public bool isOpen;
    }
}
