using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    // ------------------------------------------------------------
    // Defining attributes
    // ------------------------------------------------------------


    // Game has started or not started yet
    public bool startPlaying;

    // Game is paused or not - set to not paused initially
    private bool isPaused = false;

    // Variable to keep track of the playback position of the audio clip
    private float audioClipPosition = 0f;

    // Music in scene
    private AudioSource theMusic;

    // public AudioSource winMusic;
    // public AudioSource winFX;
    // public AudioSource loseFX;
    // public AudioSource missSound;

    // Keeping track of scenes
    int sceneIndex;

    // Screens to show on triggering certain scenarios
    // public GameObject pauseMenuPanel;
    public GameObject winPanel;
    public GameObject gameOverPanel;


    // ------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        // Set music to the audio source defined by the audio manager in scene
        theMusic = AudioManager.instance.bgMusic;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Handling pausing and resuming game
        // Input to trigger pause menu set to menu button on left controller
        // If the menu button was pressed, game pause/unpause
        if (ControlManager.instance.pauseButton.action.WasPressedThisFrame() /*Input.GetKeyDown(KeyCode.P)*/)
        {
            if (isPaused)
            {
                // If the game is already paused, the game will be resumed 
                ResumeGame();
            }
            else
            {
                // If the game is not paused, the game will be paused
                PauseGame();
            }
        }

    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        // Menu manager will handle showing pause menu
        // pauseMenuPanel.SetActive(true);

        audioClipPosition = theMusic.time;
        theMusic.Pause();

        Debug.Log ("Game has been paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        // Menu manager will handle disabling pause menu
        // pauseMenuPanel.SetActive(false);

        theMusic.time = audioClipPosition;
        theMusic.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}