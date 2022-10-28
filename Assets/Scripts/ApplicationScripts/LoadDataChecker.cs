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
            switch (LoadData.saveDataID)
            {
                case 1:
                    saveSystem.Load1();
                    break;
                case 2:
                    saveSystem.Load2();
                    break;
                case 3:
                    saveSystem.Load3();
                    break;
                case 4:
                    saveSystem.Load4();
                    break;
                case 5:
                    saveSystem.Load5();
                    break;
                case 6:
                    saveSystem.LoadCheckpoint();
                    break;
            }
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
