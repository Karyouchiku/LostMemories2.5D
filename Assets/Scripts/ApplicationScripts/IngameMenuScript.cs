using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class IngameMenuScript : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    //Delete Later start
    //This is just for keyboard controls not for mobile
    public GameObject ingameUI;
    public Button pauseBtn;
    public Button resumeBtn;
    public Button inventoryButton;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ingameUI.activeSelf)
            {
                pauseBtn.onClick.Invoke();
            }
            else
            {
                resumeBtn.onClick.Invoke();
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            if (ingameUI.activeSelf)
            {
                inventoryButton.onClick.Invoke();
            }
        }

        loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, target, 3 * Time.deltaTime);
    }
    //Delete Later End


    public Slider loadingProgress;
    float target;

    public void LoadScene(int sceneID)
    {
        LoadSceneAsync(sceneID);
    }

    async void LoadSceneAsync(int sceneID)
    {
        var scene = SceneManager.LoadSceneAsync(sceneID);
        scene.allowSceneActivation = false;
        do
        {
            target = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
    }
    
}
