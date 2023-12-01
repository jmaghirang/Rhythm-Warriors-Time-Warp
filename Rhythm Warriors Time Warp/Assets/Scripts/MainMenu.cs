using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    int sceneIndex;
    
    public void PlayGame()
    {
        sceneIndex = SceneManager.GetActiveScene (). buildIndex;
        if (sceneIndex == 5) {
            sceneIndex = 0;
        }
        SceneManager.LoadScene (sceneIndex+1);

        audioSource.Play();

    }

    public void Start()
    {
        sceneIndex = SceneManager.GetActiveScene (). buildIndex;
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}