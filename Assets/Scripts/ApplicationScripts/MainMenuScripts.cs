using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class MainMenuScripts : MonoBehaviour, ISaveable
{
    SaveSystemInMainMenu saveSystem;
    [Header("LoadingScreen Data")]
    public GameObject loadingScreen;
    public Slider loadingProgress;
    public TMP_Text loadingText;
    [Header("For Menu")]
    public GameObject ContinueBtn;
    public GameObject LoadGameBtn;
    public GameObject InputPlayerNameObject;
    public GameObject playTutorial;
    public TMP_Text errorMsg;
    public GameObject[] loadGameSlots;
    [Header("Radio Gender")]
    public ToggleGroup gender;

    [Header("For Options")]
    public Slider Volume;
    void Awake()
    {
        //Identify if going here is for loading save game only
        if (LoadDataCheckerInMainMenu.isThisLoadGame)
        {
            LoadDataCheckerInMainMenu.isThisLoadGame = false;
            LoadGame(LoadDataCheckerInMainMenu.saveGameID);
        }

        //Determining if there is the file name lm0
        if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[5]))
        {
            ContinueBtn.SetActive(true);
        }

        //If there is any save game file existed, avail the loadgame btn
        for (int i = 0; i < LoadData.SaveDatas.Length-1; i++)
        {
            if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[i]))
            {
                LoadGameBtn.SetActive(true);
                break;
            }

        }
        //Specific available saved game slot only
        for (int i = 0; i < LoadData.SaveDatas.Length-2; i++)
        {
            if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[i]))
            {
                loadGameSlots[i].SetActive(true);
            }
            else
            {
                loadGameSlots[i].SetActive(false);
            }

        }

        //For Settings
        saveSystem = GetComponent<SaveSystemInMainMenu>();
        if (LoadData.SaveGameFileChecker(LoadData.SaveDatas[6]))
        {
            saveSystem.LoadMainMenuSettings();
        }
        else
        {
            MenuStaticVariables.soundVolume = 1;
        }
        Volume.value = MenuStaticVariables.soundVolume;
    }
    void Update()
    {
        
        if (loadingScreen.activeSelf)
        {
            loadingText.text = $"Loading: {(int)(LoadingScreenScript.target * 100)}%";
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
    public void CheckSaveInputPlayerName()
    {
        string inputedPlayerName = InputPlayerNameObject.GetComponentInChildren<TMP_InputField>().text;
        if (inputedPlayerName.Length < 3)
        {
            errorMsg.text = "Invalid Name";
            return;
        }
        PlayerName.playerName = inputedPlayerName;
        Toggle genderValue = gender.ActiveToggles().FirstOrDefault();
        
        Debug.Log($"Gender: {genderValue.GetComponentInChildren<Text>().text}");

        PlayerName.gender = gender.ActiveToggles().FirstOrDefault().GetComponentInChildren<Text>().text;
        playTutorial.SetActive(true);
    }
    public void LoadScene(int loadSceneID)
    {
        InputPlayerNameObject.SetActive(false);
        loadingScreen.SetActive(true);

        LoadData.isOnLoadGameData = false;

        //StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(loadSceneID));
        LoadingScreenScript.LoadScene(loadSceneID);
    }

    public void BackFromPlayerNameInputWindow()
    {
        InputPlayerNameObject.GetComponentInChildren<TMP_InputField>().text = "";
    }

    public void LoadGame(int saveID)
    {
        if (LoadData.SaveGameDataID(saveID))
        {
            loadingScreen.SetActive(true);
            //StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(2));
            LoadingScreenScript.LoadScene(2);
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
