using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour, IPuzzle, ISaveable
{
    public bool thisPuzzleDone;
    [Header("For Disabling Controls")]
    public GameObject player;
    public GameObject InGameUI;

    [Header("Portal Door to be Available")]
    public PortalDoor door;
    public GameObject[] otherGameObjects;

    public Transform[] LocationR;//Move to position when in Restricted area
    Vector3 LocationRVec;
    bool movePlayer;
    float playerMoveSpeed;
    Vector3 animationVec;

    PlayerInventory playerInventory;
    public SOItemData[] requiredItems;
    bool[] isRequiredItemsAquired;

    void Start()
    {
        playerInventory = GameObject.FindWithTag("Player Inventory").GetComponent<PlayerInventory>();
        isRequiredItemsAquired = new bool[requiredItems.Length];
    }
    void Update()
    {
        if (!thisPuzzleDone)
        {
            if (movePlayer)
            {
                animationVec = LocationRVec - player.transform.position;
                LocationRVec.y = player.transform.position.y;
                player.transform.position = Vector3.MoveTowards(player.transform.position, LocationRVec, playerMoveSpeed * Time.deltaTime);
                player.gameObject.GetComponent<CharacterAnimation>().moveX = animationVec.x;
                player.gameObject.GetComponent<CharacterAnimation>().moveZ = animationVec.z;
            }
            if (CheckInventoryForRequiredItems())
            {
                door.locked = false;
            }
        }
        else
        {
            gameObject.SetActive(false);
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
    /*
    bool CheckIsRequiredItemsAquired(bool isRequiredItemAquired)
    {
        if (isRequiredItemAquired)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */

    public void MovePlayer(bool move)
    {
        movePlayer = move;
    }
    public void MovePlayer(bool move, float moveSpeed)
    {
        movePlayer = move;
        playerMoveSpeed = moveSpeed;
    }
    public void RestrictedArea(int MoveToPositionID)
    {
        DisableControls(false);
        LocationRVec = LocationR[MoveToPositionID].position;
        MovePlayer(true, 0.8f);
    }
    public void DisableControls(bool turn)
    {
        player.GetComponent<PlayerControls>().enabled = turn;
        player.GetComponent<CharacterAnimation>().ResetAnimation();
        InGameUI.SetActive(turn);
    }

    void EnableOtherGameObjects()
    {
        for (int i = 0; i < otherGameObjects.Length; i++)
        {
            otherGameObjects[i].SetActive(true);
        }
    }
    void UnlockTheDoor()
    {
        door.locked = false;
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