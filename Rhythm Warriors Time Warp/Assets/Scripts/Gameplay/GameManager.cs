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

    // public reference to the CameraShake script
    public CameraShake cameraShake;

    // reference to the HapticFeedback script
    public HapticFeedback hapticFeedback;

    // reference to the MissDisplay script
    //public MissDisplay missDisplay;

    public Player player;

    public bool startPlaying; // Game has started or not started yet
    public bool isPaused = false; // Game is paused or not - set to not paused initially

    private float audioClipPosition = 0f; // Variable to keep track of the playback position of the audio clip

    private AudioSource theMusic; // Music in scene

    int sceneIndex; // Keeping track of scenes

    private int previousMissCounter = 0; // previous value of missCounter

    // Screens to show on triggering certain scenarios
    public GameObject winPanel;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Set music to the audio source defined by the audio manager in scene
        theMusic = AudioManager.instance.bgMusic;

        // Scene index set to current scene
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Set time scale to 1
        Time.timeScale = 1f;

        // Find and assign the MissDisplay script
        //missDisplay = GameObject.FindObjectOfType<MissDisplay>();
        //if (missDisplay == null)
        //{
        //    Debug.LogError("MissDisplay script not found in the scene.");
        //}
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

        // If player health delpletes or player misses more than 20 times, show game over screen
        if (player.currentHealth < 1 || ScoreManager.instance.GetCurrentMisses() > 20)
        {
            TriggerGameOver();
        }

        // If the song has finished playing and the player has reached a score above 10
        if (!SongManager.instance.audioSource.isPlaying && !isPaused && ScoreManager.instance.GetCurrentMisses() > 10)
        {
            TriggerGameWin();
        }

        // check if missCounter has increased
        if (ScoreManager.instance.GetCurrentMisses() > previousMissCounter)
        {
            // start the camera shake
            cameraShake.StartShake(0.5f, 0.2f, 10.0f);

            // start the haptic feedback
            hapticFeedback.PlayerGotHit();
            
            // update previousMissCounter
            previousMissCounter = ScoreManager.instance.GetCurrentMisses();
        }

        //missDisplay.UpdateMissCount(missCounter);
    }

    private void PauseGame()
    {
        // Stop the game
        Time.timeScale = 0f;
        isPaused = true;
        
        // Pause music
        audioClipPosition = theMusic.time;
        theMusic.Pause();

        Debug.Log ("Game has been paused");
    }

    public void ResumeGame()
    {
        // Set time scale back to 1
        Time.timeScale = 1f;
        isPaused = false;

        // Resume music
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