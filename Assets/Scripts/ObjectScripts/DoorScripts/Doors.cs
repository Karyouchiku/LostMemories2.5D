using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //Testing light switch
    public Light lightSwitch;
    public bool isLightOn;

    public PlayerInventory inventory;
    public SOItemData key;
    public bool locked;
    public AudioClip doorShut;
    public AudioClip doorOpen;
    public AudioClip doorKnocking;

    AudioSource audioSource;
   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Door()
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
    public void UnlockedDoor()
    {
        Debug.Log("Gettin' Fool");
        playAudio(doorOpen, 0.7f);

        if (isLightOn)
        {
            lightSwitch.range = 0;
        }
        else
        {
            lightSwitch.range = 5;
        }
        isLightOn = !isLightOn;

    }
    
    public void LockedDoor()
    {
        Debug.Log("U r here");
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

            playAudio(doorShut,0.5f);
        }
    }

    //For Playing SFX
    void playAudio(AudioClip clip, float vol)
    {
        audioSource.volume = vol;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
