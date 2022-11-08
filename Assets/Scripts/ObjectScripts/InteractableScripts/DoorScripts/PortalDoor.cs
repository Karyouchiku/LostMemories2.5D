using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour, ISaveable, IInteractor
{
    public static event HandledTriggerInteractBtn OnTriggerExitBtn;
    public delegate void HandledTriggerInteractBtn();

    public static event ItemHandler RemoveFromInv;
    public delegate void ItemHandler(SOItemData key);

    [Header("Tick for CutScenes")]
    public bool CutSceneDoor;

    PlayerInventory inventory;
    [Header("Door Info")]
    public SOItemData key;
    public bool locked;

    [Header("Audio Clips")]
    public AudioClip doorShut;
    public AudioClip doorOpen;
    public AudioClip doorKnocking;

    AudioSource audioSource;
    private void Awake()
    {
        inGameUi = GameObject.Find("IngameUI");
        playerInventory = GameObject.Find("Inventory").GetComponent<PlayerInventory>();
    }
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player Inventory").GetComponent<PlayerInventory>();
        audioSource = GameObject.Find("OtherSFX").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Burito");
        transition = GameObject.Find("Canvas").GetComponent<BlackTransitioning>();
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
        if (doorOpen != null)
        {
            playAudio(doorOpen, 0.7f);
        }
        StartCoroutine(MovePosition());
    }

    void LockedDoor()
    {
        Debug.Log("U r here");
        if (key != null)
        {
            if (!inventory.SearchItemInInventory(key))
            {

                playAudio(doorKnocking, 0.4f);
                Debug.Log("The Door is Lock");
            }
            else
            {
                Debug.Log("Door Unlocked");
                RemoveFromInv?.Invoke(key);
                locked = false;

                playAudio(doorShut, 0.5f);
            }
        }
    }

    //[Header("Player Gameobject")]
    GameObject player;
    GameObject inGameUi;
    [Header("Teleport to other position")]
    public GameObject changePositionTo;
    BlackTransitioning transition;
    Vector3 changePositionToVec;

    WorldActiveSaveState worldRenderer;
    [Header("Enable Directional Light")]
    public bool lighting;
    [Header("World to Render")]
    public int renderPlaceID;

    PlayerInventory playerInventory;
    IEnumerator MovePosition()
    {
        changePositionToVec = changePositionTo.transform.position;
        player.GetComponent<PlayerControls>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<CharacterAnimation>().ResetAnimation();
        transition.StartTransition();
        yield return new WaitForSeconds(1f);
        worldRenderer.RenderWorlds(lighting, renderPlaceID);
        player.transform.position = changePositionToVec;
        player.GetComponent<CharacterController>().enabled = true;
        if (!CutSceneDoor)
        {
            player.GetComponent<PlayerControls>().enabled = true;
            inGameUi.SetActive(true);
            playerInventory.InventoryRefresher();
        }
        OnTriggerExitBtn?.Invoke();
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
