using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataChecker : MonoBehaviour
{
    SaveSystem saveSystem;
    BlackTransitioning transition;
    [Header("Data Needed for NewGame")]
    public GameObject player;
    public GameObject ingameUI;
    public GameObject TriggerCutScene1;
    [Header("For Debugging")]
    public bool isDebugging;

    //bool isUIEnabled;
    //bool isPlayerControlsEnabled;
    void Awake()
    {
        transition = GetComponent<BlackTransitioning>();
        saveSystem = GetComponent<SaveSystem>();
        if (!isDebugging)
        {
            if (LoadData.isOnLoadGameData)
            {
                //Check When this is a Load Game
                //LoadData.isOnLoadGameData = false;
                Debug.Log($"LoadData.isOnLoadGameData is {LoadData.isOnLoadGameData}");
                StartCoroutine(CheckIfLoadGame());
            }
            else
            {
                //This is for New Game
                TriggerCutScene1.SetActive(true);
                player.GetComponent<PlayerControls>().enabled = false;
                //ingameUI.SetActive(false);
                transition.StartTransition2ndVer();
            }
        }
        else
        {
            MenuStaticVariables.soundVolume = 1f;
        }
    }
    IEnumerator CheckIfLoadGame()
    {
        transition.StartTransition2ndVer();
        yield return new WaitForFixedUpdate();
        player.GetComponent<CharacterController>().enabled = false;
        saveSystem.Load(LoadData.saveDataID);
        player.GetComponent<CharacterController>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        transition.ManualTransitionOFF();
        player.GetComponent<PlayerControls>().enabled = true;
        ingameUI.SetActive(true);

    }
}