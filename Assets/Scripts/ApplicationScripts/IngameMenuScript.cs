using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameMenuScript : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

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
    }
}
