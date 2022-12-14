using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataChecker : MonoBehaviour
{
    SaveSystem saveSystem;
    BlackTransitioning trasitioning;
    [Header("Data Needed for NewGame")]
    public GameObject player;
    public GameObject ingameUI;
    public GameObject movePlayerLocation;
    public bool movePlayer;
    Vector3 target;
    [Header("For Debugging")]
    public bool isDebugging;
    void Awake()
    {
        trasitioning = GameObject.Find("Canvas").GetComponent<BlackTransitioning>();
        saveSystem = GetComponent<SaveSystem>();
        if (!isDebugging)
        {
            if (LoadData.isOnLoadGameData)
            {
                //Check When this is a Load Game
                saveSystem.Load(LoadData.saveDataID);
                player.GetComponent<PlayerControls>().enabled = true;
            
            }
            else
            {
                //This is for New Game
                player.GetComponent<PlayerControls>().enabled = false;
                ingameUI.SetActive(false);
                movePlayer = true;
                trasitioning.StartTransition2ndVer();
            }
        }
        else
        {
            MenuStaticVariables.soundVolume = 1f;
        }
    }
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
