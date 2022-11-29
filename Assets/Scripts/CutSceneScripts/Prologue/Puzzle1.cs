using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puzzle1 : MonoBehaviour, IPuzzle, ISaveable
{
    BackgroundMusicScript bgm;
    public bool startThisPuzzle;
    public bool thisPuzzleDone;
    [Header("For Disabling Controls")]
    public GameObject player;
    public GameObject InGameUI;

    [Header("Portal Door to be Available")]
    public PortalDoor[] portalDoors;
    public GameObject[] GameObjectChildren;
    public GameObject[] otherGameObjects;

    public Transform[] LocationsToMove;//Move to position
    Vector3 LocationToMove;
    bool movePlayer;
    float playerMoveSpeed;
    Vector3 animationVec;

    PlayerInventory playerInventory;
    public SOItemData[] requiredItems;
    bool[] isRequiredItemsAquired;

    BlackTransitioning transition;
    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BackgroundMusicScript>();
        transition = GameObject.Find("Canvas").GetComponent<BlackTransitioning>();
        playerInventory = GameObject.FindWithTag("Player Inventory").GetComponent<PlayerInventory>();
        isRequiredItemsAquired = new bool[requiredItems.Length];
        PuzzleChilds(false);
        EnableInteractableObjects(false);

    }
    void Update()
    {
        if (startThisPuzzle)
        {

            if (!thisPuzzleDone)
            {
                PuzzleChilds(true);
                EnableOtherGameObjects();
                if (movePlayer)
                {
                    animationVec = LocationToMove - player.transform.position;
                    LocationToMove.y = player.transform.position.y;
                    player.transform.position = Vector3.MoveTowards(player.transform.position, LocationToMove, playerMoveSpeed * Time.deltaTime);
                    player.gameObject.GetComponent<CharacterAnimation>().moveX = animationVec.x;
                    player.gameObject.GetComponent<CharacterAnimation>().moveZ = animationVec.z;
                }

                if (CheckInventoryForRequiredItems())
                {
                    UnlockTheDoor();
                }
            }
            else
            {
                PuzzleChilds(false);
            }
        }
        
    }
    void PuzzleChilds(bool isEnable)
    {
        for (int i = 0; i < GameObjectChildren.Length; i++)
        {
            GameObjectChildren[i].gameObject.SetActive(isEnable);
        }
        /*
        if (!isEnable)
        {
            startThisPuzzle = false;
        }
        */
    }
    bool objectiveIsOngoing = true;
    bool CheckInventoryForRequiredItems()
    {
        bool[] checkSearch = new bool[isRequiredItemsAquired.Length];
        int objectiveItemCount = requiredItems.Length;
        for (int i = 0; i < requiredItems.Length; i++)
        {
            checkSearch[i] = playerInventory.SearchItemInInventory(requiredItems[i]);

            if (playerInventory.SearchItemInInventory(requiredItems[i]))
            {
                if (objectiveIsOngoing)
                {
                    if (objectiveItemCount > 1)
                    {
                        objectiveItemCount--;
                        IObjectives.SetObjective1($"Item Left: {objectiveItemCount}");

                    }
                    else
                    {
                        objectiveIsOngoing = false;
                        IObjectives.SetObjective1("");
                        IQuest.SetQuest("Get out of the House");
                    }
                }
            }
        }
        
        if (Array.TrueForAll(checkSearch, 
            CheckIsRequiredItemsAquired =>
                {
                    if (CheckIsRequiredItemsAquired)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            )
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //IPuzzle Methods
    public void MovePlayer(bool move)
    {
        movePlayer = move;
        DisableControls(true);
    }
    public void MovePlayer(bool move, float moveSpeed, int MoveToPositionID)
    {
        movePlayer = move;
        playerMoveSpeed = moveSpeed;
        LocationToMove = LocationsToMove[MoveToPositionID].position;
        DisableControls(false);
    }
    public void DisableControls(bool turn)
    {
        player.GetComponent<PlayerControls>().enabled = turn;
        player.GetComponent<PlayerControls>().ResetJoystickValue();
        player.GetComponent<CharacterAnimation>().ResetAnimation();
        InGameUI.SetActive(turn);
    }
    public void FinishingPuzzle()
    {
        transition.ManualTransitionON();
        StartCoroutine(TeleportPlayer());
    }
    IEnumerator TeleportPlayer()
    {
        yield return new WaitForSeconds(2);
        DisableControls(false);
        portalDoors[1].Interact();
        yield return new WaitForSeconds(1);
        thisPuzzleDone = true;
    }
    //Unique Methods
    void EnableOtherGameObjects()
    {
        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }
        otherGameObjects[1].tag = "Untagged";
        otherGameObjects[1].SetActive(false);
    }
    public void EnableInteractableObjects(bool turn)
    {
        string tag = turn ? "InteractableObject" : "Untagged";
        foreach (InteractableItem item in otherGameObjects[2].GetComponentsInChildren<InteractableItem>())
        {
            item.tag = tag;
        }
        otherGameObjects[3].tag = "InteractableObject";
    }

    void UnlockTheDoor()
    {
        portalDoors[0].locked = false;
    }
    public object SaveState()
    {
        return new SaveData()
        {
            thisPuzzleDone = this.thisPuzzleDone,
            startThisPuzzle = this.startThisPuzzle
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisPuzzleDone = saveData.thisPuzzleDone;
        this.startThisPuzzle = saveData.startThisPuzzle;
        StartCoroutine(EnabllingIOForLoadGame());

        
    }
    IEnumerator EnabllingIOForLoadGame()
    {
        yield return new WaitForFixedUpdate();
        if (startThisPuzzle && !thisPuzzleDone)
        {
            EnableInteractableObjects(true);
        }
    }

    [Serializable]
    struct SaveData
    {
        public bool thisPuzzleDone;
        public bool startThisPuzzle;
    }
}