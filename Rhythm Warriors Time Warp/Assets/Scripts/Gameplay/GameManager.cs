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
    private int currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Player player;

    public bool startPlaying; // Game has started or not started yet
    public bool isPaused = false; // Game is paused or not - set to not paused initially

    private float audioClipPosition = 0f; // Variable to keep track of the playback position of the audio clip

    private AudioSource theMusic; // Music in scene

    // Start is called before the first frame update
    void Start()
    {
        // Set music to the audio source defined by the audio manager in scene
        theMusic = AudioManager.instance.bgMusic;

        // Set time scale to 1
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Handling pausing and resuming game
        if (ControlManager.instance.pauseButton.action.WasPressedThisFrame() && !InventoryManager.instance.inventory.gameObject.activeSelf)
        {
            MenuManager.instance.TriggerPauseMenu();

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

        if (player.currentHealth < 1)
        {
            MenuManager.instance.TriggerGameOverPanel();

            TriggerGameOver();
        }
    }

    private void PauseGame()
    {
        VFXManager.instance.DisableEffects();

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
        VFXManager.instance.EnableEffects();

        // Set time scale back to 1
        Time.timeScale = 1f;
        isPaused = false;

        // Resume music
        theMusic.time = audioClipPosition;
        theMusic.Play();
    }

    public void TriggerGameOver()
    {
        VFXManager.instance.DisableEffects();

        Time.timeScale = 0f;
        SongManager.instance.StopSong();
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }

    /* for save/load functionality */
    public void LoadPlayerProgress(SaveData saveData)
    {
        // update player progress based on saveData
        currentLevel = saveData.currentLevel;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
}