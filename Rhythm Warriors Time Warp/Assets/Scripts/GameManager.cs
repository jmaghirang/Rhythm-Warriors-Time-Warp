using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // variable to keep track of the playback position of the audio clip
    private float audioClipPosition = 0f;
    public AudioSource theMusic;

    // i am so sorry there are so many audio variable
    public AudioSource winMusic;
    public AudioSource winFX;
    public AudioSource loseFX;
    //public AudioSource missSound;

    public bool startPlaying;

    public static GameManager instance;

    int sceneIndex;

    public GameObject pauseMenuPanel;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuPanel.SetActive(true);

        audioClipPosition = theMusic.time;
        theMusic.Pause();

        Debug.Log ("Game has been paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuPanel.SetActive(false);

        theMusic.time = audioClipPosition;
        theMusic.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }

}