using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    private int comboCount = 0;
    private int comboMultiplier = 1;

    // update the score based on successful actions
    public void UpdateScore(int scoreToAdd)
    {
        currentScore += scoreToAdd * comboMultiplier;
        // update UI or perform other actions to reflect the changed score
    }

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
