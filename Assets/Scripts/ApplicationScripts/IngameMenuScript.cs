using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenuScript : MonoBehaviour
{
    public Slider loadingProgress;
    public GameObject loadingScreen;

    void Start()
    {
        AudioListener.volume = MenuStaticVariables.soundVolume;
        RestoreDDB();
    }
    
    void Update()
    {
        if (loadingScreen.activeSelf)
        {
            loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, 3 * Time.deltaTime);
        }
    }
    [Header("Restore backup DDB")]
    public DialogueModifier dialogueModifier;
    public SaveValuesInDDB saveValues;
    public void BackToMainMenu()
    {
        StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(1));
    }
    public void RestoreDDB()
    {
        dialogueModifier.RestoreDialogues();
        saveValues.RestoreValues();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
