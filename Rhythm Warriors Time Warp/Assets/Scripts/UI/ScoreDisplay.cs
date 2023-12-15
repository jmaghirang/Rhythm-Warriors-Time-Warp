using UnityEngine;
using TMPro;

// Some code referenced from:
// https://www.youtube.com/watch?v=TAGZxRMloyU&ab_channel=Brackeys
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!"); // debugging
        }
    }

    void Update()
    {
        if (scoreManager != null)
        {
            scoreText.text = "Score: " + scoreManager.GetCurrentScore().ToString();
        }
    }
}