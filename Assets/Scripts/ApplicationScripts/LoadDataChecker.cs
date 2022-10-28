using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataChecker : MonoBehaviour
{
    SaveSystem saveSystem;
    BlackTransitioning trasitioning;
    public GameObject player;
    public GameObject ingameUI;
    public bool movePlayer;
    public GameObject movePlayerLocation;
    void Awake()
    {
        trasitioning = GameObject.Find("Canvas").GetComponent<BlackTransitioning>();
        saveSystem = GetComponent<SaveSystem>();
        if (LoadData.isOnLoadGameData)
        {
            saveSystem.Load(LoadData.saveDataID);
            player.GetComponent<PlayerControls>().enabled = true;
            
        }
        else
        {
            player.GetComponent<PlayerControls>().enabled = false;
            ingameUI.SetActive(false);
            movePlayer = true;
            trasitioning.StartTransition2ndVer();
        }
    }
    Vector3 target;
    void FixedUpdate()
    {
        
        if (movePlayer)
        {
            target = movePlayerLocation.transform.position;
            target.y = player.transform.position.y;

            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 2 * Time.deltaTime);
        }
    }
}
