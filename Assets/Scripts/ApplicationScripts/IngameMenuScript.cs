using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenuScript : MonoBehaviour
{
    

    public Slider loadingProgress;
    //Delete Later start
    //This is just for keyboard controls not for mobile
    public GameObject ingameUI;
    public Button pauseBtn;
    public Button resumeBtn;
    public Button inventoryButton;

    void KeyboardControls()
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
    }
    //Delete Later End
    void Update()
    {
        //Delete this line too
        KeyboardControls();

        loadingProgress.value = Mathf.MoveTowards(loadingProgress.value, LoadingScreenScript.target, 3 * Time.deltaTime);
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
