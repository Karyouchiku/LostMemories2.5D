using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public GameObject warningScreen;
    public GameObject blackFade;
    public GameObject ageConfirmationScreen;

    void Start()
    {
        StartCoroutine(SplashTransition());
    }
    IEnumerator SplashTransition()
    {
        yield return new WaitForSeconds(4);
        blackFade.GetComponent<Animator>().SetBool("fadeOut",true);
        yield return new WaitForSeconds(2);
        warningScreen.SetActive(false);
        blackFade.SetActive(false);
        ageConfirmationScreen.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
