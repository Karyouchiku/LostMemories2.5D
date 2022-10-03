using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    //Testing light switch
    public Light lightSwitch;
    public bool isLightOn;

    public PlayerInventory inventory;
    public ItemData Key;
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
        playAudio(doorOpen);

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
        if (!inventory.SearchItemInInventory(Key))
        {
            playAudio(doorKnocking);
            Debug.Log("The Door is Lock");
        }
        else
        {
            Debug.Log("Door Unlocked");
            inventory.Remove(Key);
            locked = false;

            playAudio(doorShut);
        }
    }
    void playAudio(AudioClip clip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
