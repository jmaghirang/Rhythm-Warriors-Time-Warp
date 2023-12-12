using UnityEngine;
using TMPro;

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
            Debug.LogError("ScoreManager not found in the scene!");
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