using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuScripts : MonoBehaviour
{
    public Slider loadingProgress;
    public GameObject loadingScreen;

    void Update()
    {
        
        if (loadingScreen.activeSelf)
        {
            loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, Time.deltaTime);
        }
    }
    public void NewGame()
    {
        StartCoroutine(LoadingScreenScript.LoadScene_Coroutine(2));
    }
    public void LoadGame()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
