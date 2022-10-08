using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    
    public void OpenScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseScene()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void WarningTransition()
    {

    }
}
