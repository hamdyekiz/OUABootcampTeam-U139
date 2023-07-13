using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject popUpUI;
    public GameObject underDevelopmentUI;

    private bool isPopupShowing = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public void underDevelopment()
    {
        if (!isPopupShowing)
        {
            isPopupShowing = true;
            StartCoroutine(ShowPopupForDuration(1f));
            underDevelopmentUI.SetActive(true);
        }
    }

    public void gameOver()
    {
        Invoke("ShowGameOverScreen", 1.5f);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOverScreen()
    {
        gameOverUI.SetActive(true);
    }

    IEnumerator ShowPopupForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        underDevelopmentUI.SetActive(false);
        isPopupShowing = false;
    }
}