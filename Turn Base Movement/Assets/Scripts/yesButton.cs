using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class yesButton : MonoBehaviour
{
    public GameManagerScript gameManager;
    public GameObject LoadingScreen;
    public Slider Loading;

    public void TransitionToNextScene()
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        Loading.value = 0;
        LoadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            Loading.value = progress;
            if (progress >= 0.9f)
            {
                Loading.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        Loading.value = 1f;
        LoadingScreen.SetActive(false);

    }
}