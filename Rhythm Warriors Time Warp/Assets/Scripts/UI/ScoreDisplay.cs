using UnityEngine;
using TMPro;

// Some code referenced from:
// https://www.youtube.com/watch?v=TAGZxRMloyU&ab_channel=Brackeys
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        if (ScoreManager.instance == null)
        {
            Debug.LogError("ScoreManager not found in the scene!"); // debugging
        }
    }

    void Update()
    {
        if (ScoreManager.instance != null)
        {
            scoreText.text = "Score: " + ScoreManager.instance.GetCurrentScore().ToString();
        }
    }
}