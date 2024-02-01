using System.Collections;
using System.Collections.Generic;
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

    private int currentScore = 0;
    private int currentMisses = 0;

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
