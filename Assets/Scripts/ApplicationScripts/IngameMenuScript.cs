using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameMenuScript : MonoBehaviour
{
    [Header("Loading Screen Data")]
    public GameObject loadingScreen;
    public Slider loadingProgress;
    public TMP_Text loadingText;

    [Header("Restore backup DDB")]
    public DialogueModifier dialogueModifier;
    void Start()
    {
        AudioListener.volume = MenuStaticVariables.soundVolume;
        dialogueModifier.RestoreDialogues();
    }
    
    void Update()
    {
        if (loadingScreen.activeSelf)
        {
            loadingText.text = $"Loading: {(int)(LoadingScreenScript.target * 100)}%";
            loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, 3 * Time.deltaTime);
        }
    }
    public void BackToMainMenu()
    {
        StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
