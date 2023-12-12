using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;

    public int GetCurrentScore()
    {
        return currentScore;
    }
    
    // update the score based on hitting enemy prefab
    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; // increase the score by the specified amount
        Debug.Log("Score updated. Current score: " + currentScore); // debugging
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
