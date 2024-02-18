using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;


// Some code referenced from:
// https://www.youtube.com/watch?v=TAGZxRMloyU&ab_channel=Brackeys
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    // public reference to the CameraShake script
    public CameraShake cameraShake;

    // reference to the HapticFeedback script
    public HapticFeedback hapticFeedback;

    private int currentScore = 0;
    private int currentMisses = 0;

    private int previousMissCounter = 0; // previous value of missCounter

    void Update()
    {
        LoseCondition();
        WinCondition();
        OnMiss();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetCurrentMisses()
    {
        return currentMisses;
    }
    
    // update the score based on hitting enemy prefab
    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; // increase the score by the specified amount

        AudioManager.instance.hitSFX.Play();
        Debug.Log("Hit");
        Debug.Log("Score updated. Current score: " + currentScore); // debugging
    }

    public void UpdateMisses(int missesToAdd)
    {
        currentMisses += missesToAdd;

        AudioManager.instance.missSFX.Play();
        Debug.Log("Miss");
        Debug.Log("Misses updated. Current misses: " + currentMisses);
    }

    //////

    public void LoseCondition()
    {
        if (GameManager.instance.player.currentHealth < 1 || GetCurrentMisses() > 20)
        {
            GameManager.instance.TriggerGameOver();
        }
    }

    public void WinCondition()
    {
        if (!SongManager.instance.audioSource.isPlaying && !GameManager.instance.isPaused && GetCurrentScore() > 10)
        {
            GameManager.instance.TriggerGameWin();
        }
    }

    public void OnMiss()
    {
        // check if missCounter has increased
        if (GetCurrentMisses() > previousMissCounter)
        {
            // start the camera shake
            cameraShake.StartShake(0.5f, 0.2f, 10.0f);

            // start the haptic feedback
            hapticFeedback.PlayerGotHit();

            // update previousMissCounter
            previousMissCounter = ScoreManager.instance.GetCurrentMisses();
        }
    }
}

    /* add combos later on
    // increase combo count
    public void IncreaseCombo()
    {
        comboCount++;
        UpdateComboMultiplier(); // Update the combo multiplier when the combo count increases
    }

    // reset combo count
    public void ResetCombo()
    {
        comboCount = 0;
        comboMultiplier = 1;
    }

    // update combo multiplier based on combo count
    private void UpdateComboMultiplier()
    {
        // combo multiplier based on the combo count
        if (comboCount >= 5)
        {
            comboMultiplier = 2;
        }
    }
}
*/
