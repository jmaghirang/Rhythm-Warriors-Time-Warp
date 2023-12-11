using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText; // reference to the UI text element displaying health
    public Text scoreText; // reference to the UI text element displaying score

    private void Start()
    {
        // initialize UI elements, retrieve references, etc.
        healthText.text = "Health: ";
        scoreText.text = "Score: ";
    }

    // update the health display with current health value
    public void UpdateHealthDisplay(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    // update the score display with current score value
    public void UpdateScoreDisplay(int currentScore)
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}