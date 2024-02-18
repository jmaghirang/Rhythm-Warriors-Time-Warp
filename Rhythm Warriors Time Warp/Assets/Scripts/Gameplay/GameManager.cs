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
        if (ControlManager.instance.pauseButton.action.WasPressedThisFrame())
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

    public void TriggerGameOver()
    {
        SceneTransitionManager.instance.LoadNextScene(5);
    }

    public void TriggerGameWin()
    {
        SceneTransitionManager.instance.LoadNextScene(4);
    }

    public void QuitGame()
    {
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}