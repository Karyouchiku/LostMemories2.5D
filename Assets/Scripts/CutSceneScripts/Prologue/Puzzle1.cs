using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour, IPuzzle, ISaveable
{
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
        transition = GameObject.Find("Canvas").GetComponent<BlackTransitioning>();
        playerInventory = GameObject.FindWithTag("Player Inventory").GetComponent<PlayerInventory>();
        isRequiredItemsAquired = new bool[requiredItems.Length];
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
        if (!isEnable)
        {
            startThisPuzzle = false;
        }
    }
    bool CheckInventoryForRequiredItems()
    {
        bool[] checkSearch = new bool[isRequiredItemsAquired.Length];
        for (int i = 0; i < requiredItems.Length; i++)
        {
            checkSearch[i] = playerInventory.SearchItemInInventory(requiredItems[i]);
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
        InGameUI.SetActive(turn);
        player.GetComponent<CharacterAnimation>().ResetAnimation();
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
    }
    void UnlockTheDoor()
    {
        portalDoors[0].locked = false;
    }
    public object SaveState()
    {
        return new SaveData()
        {
            thisPuzzleDone = this.thisPuzzleDone
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        this.thisPuzzleDone = saveData.thisPuzzleDone;
    }


    [Serializable]
    struct SaveData
    {
        public bool thisPuzzleDone;
    }
}