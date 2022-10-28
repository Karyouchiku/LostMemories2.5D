using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScripts : MonoBehaviour, ISaveable
{
    SaveSystemInMainMenu saveSystem;
    [Header("For Menu")]
    public Slider loadingProgress;
    public GameObject ContinueBtn;
    public GameObject loadingScreen;
    public GameObject InputPlayerNameObject;
    public PlayerName playerName;
    public TMP_Text errorMsg;
    [Header("For Options")]
    public Slider Volume;
    void Awake()
    {
        MenuStaticVariables.isContinueAvailable = LoadData.SaveGameFileChecker(LoadData.SaveDatas[7]);
        if (MenuStaticVariables.isContinueAvailable)
        {
            ContinueBtn.SetActive(true);
        }
        saveSystem = GetComponent<SaveSystemInMainMenu>();
        if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[6]))
        {
            saveSystem.LoadMainMenuSettings();
        }
        Volume.value = MenuStaticVariables.soundVolume;
    }
    void Update()
    {
        
        if (loadingScreen.activeSelf)
        {
            loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, Time.deltaTime);
        }
        //ChangeSoundVolume
        //MenuStaticVariables.soundVolume = Volume.value;

        AudioListener.volume = Volume.value;
        MenuStaticVariables.soundVolume = Volume.value;
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
    //For Options
    int qualityLevel;
    public void ChangeQualitySettings(int level)
    {
        QualitySettings.SetQualityLevel(level);
        qualityLevel = level;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public object SaveState()
    {
        return new SaveData()
        {
            soundVolume = MenuStaticVariables.soundVolume,
            qualityLevel = this.qualityLevel,
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        MenuStaticVariables.soundVolume = saveData.soundVolume;
        this.qualityLevel = saveData.qualityLevel;
        ChangeQualitySettings(this.qualityLevel);

    }


    [Serializable]
    struct SaveData
    {
        public float soundVolume;
        public int qualityLevel;
    }
}
