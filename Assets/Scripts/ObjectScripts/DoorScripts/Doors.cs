using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, ISaveable
{
    //Testing light switch
    public Light lightSwitch;
    public bool isLightOn;

    PlayerInventory inventory;
    [Header("Door Info")]
    public SOItemData key;
    public bool locked;

    [Header("Audio Clips")]
    public AudioClip doorShut;
    public AudioClip doorOpen;
    public AudioClip doorKnocking;
    
    
    AudioSource audioSource;
   

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventory>();
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

        try
        {
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
        catch (Exception)
        {
            Debug.Log("No lights");
        }

        StartCoroutine(MovePosition());
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
    [Header("World to Render")]
    public GameObject unrenderWorld;
    public GameObject renderWorld;
    IEnumerator MovePosition()
    {
        changePositionToVec = changePositionTo.transform.position;
        GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerControls>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerAnimations>().resetAnimation();
        GameObject.FindWithTag("Canvas").GetComponent<BlackTransitioning>().StartTransition();
        yield return new WaitForSeconds(0.8f);
        unrenderWorld.SetActive(false);
        renderWorld.SetActive(true);
        GameObject.FindWithTag("Player").transform.position = changePositionToVec;
        GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerControls>().enabled = true;
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
