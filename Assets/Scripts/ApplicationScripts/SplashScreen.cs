using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        if (!File.Exists(LoadData.SaveDatas[6]))
        {
            ageConfirmationScreen.SetActive(true);
        }
        else
        {
            GoToMainMenu();
        }
        warningScreen.SetActive(false);
        blackFade.SetActive(false);
    }

    public void GoToMainMenu()
    {
        FirstSave();
        MenuStaticVariables.soundVolume = 1;
        SceneManager.LoadScene(1);
    }

    public void FirstSave()
    {
        var state = LoadFile(LoadData.SaveDatas[6]);
        SaveState(state);
        FileStreamingManager.SaveFile(state, LoadData.SaveDatas[6]);
    }
    Dictionary<string, object> LoadFile(string SavePath)
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No Savegame Data\nCreated a new save game File");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }
}
