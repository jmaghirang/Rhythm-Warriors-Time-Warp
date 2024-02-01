using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int sceneIndex;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void PlayGame()
    {
        if (sceneIndex == 5) {
            sceneIndex = 0;
        }
        SceneManager.LoadScene (sceneIndex + 1);
    }

    public void ReloadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}