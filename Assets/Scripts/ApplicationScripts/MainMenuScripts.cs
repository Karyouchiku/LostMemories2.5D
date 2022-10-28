using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScripts : MonoBehaviour
{
    public Slider loadingProgress;
    public GameObject loadingScreen;
    public GameObject InputPlayerNameObject;
    public PlayerName playerName;
    public TMP_Text errorMsg;
    void Update()
    {
        
        if (loadingScreen.activeSelf)
        {
            loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, Time.deltaTime);
        }
    }
    public void NewGame()
    {
        InputPlayerNameObject.SetActive(true);
    }
    public void SaveInputPlayerName()
    {
        string inputedPlayerName = InputPlayerNameObject.GetComponentInChildren<TMP_InputField>().text;
        if (inputedPlayerName.Length < 3)
        {
            errorMsg.text = "Invalid Name";
            return;
        }
        playerName.playerName = inputedPlayerName;
        InputPlayerNameObject.SetActive(false);
        loadingScreen.SetActive(true);

        LoadData.isOnLoadGameData = false;
        StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(2));
    }


    public void LoadGame(int saveID)
    {
        if (LoadData.SaveGameDataID(saveID, true))
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(2));
        }
        else
        {
            Debug.Log("No Saved data");
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
