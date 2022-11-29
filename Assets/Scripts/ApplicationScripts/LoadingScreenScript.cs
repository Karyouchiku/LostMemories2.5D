using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class LoadingScreenScript
{
    public static float target;
    /*
    public static IEnumerator LoadScene_Coroutine(int sceneID)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneID);
        //asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            target = asyncOperation.progress;
            if (target >= 0.9f)
            {
                //asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    */
    public static async void LoadScene(int sceneID)
    {
        await Task.Delay(50);
        var scene = SceneManager.LoadSceneAsync(sceneID);
        scene.allowSceneActivation = false;

        do
        {
            target = scene.progress;
        } while (scene.progress < 0.9f);
        scene.allowSceneActivation = true;
    }

}
