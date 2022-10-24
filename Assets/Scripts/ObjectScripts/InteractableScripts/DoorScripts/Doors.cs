using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Doors : MonoBehaviour, ISaveable, IInteractor
{
    public static event HandledTriggerInteractBtn OnTriggerExitBtn;
    public delegate void HandledTriggerInteractBtn();

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
        player = GameObject.FindGameObjectWithTag("Burito");
        transition = GameObject.FindGameObjectWithTag("Canvas");
        worldRenderer = GetComponentInParent<WorldActiveSaveState>();
    }
    public void Interact()
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
        playAudio(doorOpen, 0.7f);
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

    //[Header("Player Gameobject")]
    GameObject player;
    [Header("Teleport to other position")]
    GameObject transition;
    public GameObject changePositionTo;
    Vector3 changePositionToVec;

    WorldActiveSaveState worldRenderer;
    [Header("World to Render")]
    public bool renderClassRoom;
    public bool renderSchoolHallway;
    public bool renderMCHouseOutside;
    public bool renderMCHouseInterior;
    public bool renderOutsideSchool;
    public bool renderSmallTown;
    public bool renderBigCity;
    public bool renderTrailerPark;
    public bool renderFlorHouse;
    public bool renderWarehouse;

    IEnumerator MovePosition()
    {
        changePositionToVec = changePositionTo.transform.position;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerControls>().enabled = false;
        player.GetComponent<PlayerAnimations>().resetAnimation();
        transition.GetComponent<BlackTransitioning>().StartTransition();
        yield return new WaitForSeconds(0.8f);

        worldRenderer.RenderWorlds(renderClassRoom, renderSchoolHallway, renderMCHouseOutside,
            renderMCHouseInterior, renderOutsideSchool, renderSmallTown, renderBigCity, renderTrailerPark,
            renderFlorHouse, renderWarehouse);

        worldRenderer.StartRender();
        player.transform.position = changePositionToVec;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerControls>().enabled = true;
        OnTriggerExitBtn?.Invoke();
        yield return null;
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
