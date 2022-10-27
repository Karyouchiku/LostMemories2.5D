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
        inGameUi = GameObject.Find("IngameUI");
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
        if (doorOpen != null)
        {
            playAudio(doorOpen, 0.7f);
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
            RemoveFromInv?.Invoke(key);
            locked = false;

            playAudio(doorShut, 0.5f);
        }
    }

    //[Header("Player Gameobject")]
    GameObject player;
    GameObject inGameUi;
    [Header("Teleport to other position")]
    GameObject transition;
    public GameObject changePositionTo;
    Vector3 changePositionToVec;

    WorldActiveSaveState worldRenderer;
    [Header("Enable Directional Light")]
    public bool lighting;
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
        player.GetComponent<CharacterAnimation>().ResetAnimation();
        transition.GetComponent<BlackTransitioning>().StartTransition();
        yield return new WaitForSeconds(1f);

        worldRenderer.RenderWorlds(lighting, renderClassRoom, renderSchoolHallway, renderMCHouseOutside,
            renderMCHouseInterior, renderOutsideSchool, renderSmallTown, renderBigCity, renderTrailerPark,
            renderFlorHouse, renderWarehouse);

        worldRenderer.StartRender();
        player.transform.position = changePositionToVec;
        player.GetComponent<PlayerControls>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        inGameUi.SetActive(true);
        yield return new WaitForSeconds(1f);
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
