using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, ISaveable
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
        audioSource = GameObject.Find("OtherSFX").GetComponent<AudioSource>();
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
    void UnlockedDoor()
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

        MovePosition();
    }
    
    void LockedDoor()
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


    [Header("Teleport to other position")]
    public GameObject changePositionTo;
    Vector3 changePositionToVec;
    //Vector3 player;

    void MovePosition()
    {
        //player = GameObject.FindWithTag("Player").transform.position;
        changePositionToVec = changePositionTo.transform.position;
        //player = changePositionToVec;
        GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled = false;
        GameObject.FindWithTag("Player").transform.position = changePositionToVec;
        GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled = true;
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

    public object SaveState()
    {
        return new SaveData()
        {
            locked = this.locked
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;

        this.locked = saveData.locked;
    }

    [Serializable]
    struct SaveData
    {
        public bool locked;
    }
}
