using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public static class LoadingScreenScript
{
    public static float target;
    public static IEnumerator LoadScene_Coroutine(int sceneID)
    {
        yield return new WaitForFixedUpdate();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneID);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            target = asyncOperation.progress;
            if (target >= 0.9f)
            {
                yield return new WaitForFixedUpdate();
                asyncOperation.allowSceneActivation = true;
            }
        }
    }
}
