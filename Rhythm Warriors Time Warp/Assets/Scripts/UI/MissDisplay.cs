using UnityEngine;
using TMPro;

public class MissDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI missCounterText; // Serialized field to make it visible in the Inspector

    void Start()
    {
        // Ensure that the TextMeshProUGUI component is assigned in the Inspector
        if (missCounterText == null)
        {
            Debug.LogError("MissCounterText not assigned in the Inspector. Please assign a TextMeshProUGUI component.");
        }
    }

    void Update()
    {
        if(ScoreManager.instance != null)
        {
            // Update missCounterText with the provided miss count
            missCounterText.text = "Misses: " + ScoreManager.instance.GetCurrentMisses().ToString();
        }
    }
}
