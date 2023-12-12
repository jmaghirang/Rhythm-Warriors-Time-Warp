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

    public Player player;

    public bool startPlaying; // Game has started or not started yet
    public bool isPaused = false; // Game is paused or not - set to not paused initially

    private float audioClipPosition = 0f; // Variable to keep track of the playback position of the audio clip

    private AudioSource theMusic; // Music in scene

    int sceneIndex; // Keeping track of scenes

    public int missCounter = 0;

    // Screens to show on triggering certain scenarios
    public GameObject winPanel;
    public GameObject gameOverPanel;


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

        Debug.Log("Misses: " + missCounter);

        if (player.currentHealth < 1 || missCounter > 20)
        {
            TriggerGameOver();
        }

        if (!SongManager.instance.audioSource.isPlaying && !isPaused && ScoreManager.instance.currentScore > 10)
        {
            TriggerGameWin();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        
        audioClipPosition = theMusic.time;
        theMusic.Pause();

        Debug.Log ("Game has been paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

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

    public void TriggerGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void TriggerGameWin()
    {
        SceneManager.LoadScene("Win Screen");
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}