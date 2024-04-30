using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// some VFX code referenced from: https://www.youtube.com/watch?v=N3JR5m7knGQ&t=86s
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float intensity = 1; // intensity of the vignette effect

    public PostProcessVolume _volume;
    Vignette _vignette;

    // reference to other scripts
    public CameraShake cameraShake;
    public HapticFeedback hapticFeedback;

    public TextMeshProUGUI accuracyText;

    private int currentScore = 0;
    private int currentMisses = 0;
    private int previousMissCounter = 0; // previous value of missCounter

    public GameObject scorePanel; // reference to the score panel
    public GameObject missPanel; // reference to the miss panel

    bool isEnabled = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //_volume = gameObject.GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings(out _vignette);

        if(!_vignette)
        {
            print("error, vignette empty");
        }

        else
        {
            _vignette.enabled.Override(false);
        }

        accuracyText.text = "";
    }

    void Update()
    {
        if (!SongManager.instance.audioSource.isPlaying)
        {
            accuracyText.text = "";
        }

        int showPanels = PlayerPrefs.GetInt("PanelsEnabled", 0);
        
        if (showPanels == 1)
        {
            isEnabled = true;
        }
        else if (showPanels == 0)
        {
            isEnabled = false;
        }

        scorePanel.SetActive(isEnabled);
        missPanel.SetActive(isEnabled);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetCurrentMisses()
    {
        return currentMisses;
    }
    
    // Update the score based on hitting enemy prefab
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
        OnMiss();
    }

    public void LateHit()
    {
        accuracyText.text = "Late";
        accuracyText.color = Color.yellow;
    }

    public void EarlyHit()
    {
        accuracyText.text = "Early";
        accuracyText.color = Color.yellow;
    }

    public void PerfectHit()
    {
        accuracyText.text = "Perfect!";
        accuracyText.color = Color.cyan;
    }

    public void MissedHit()
    {
        accuracyText.text = "Miss";
        accuracyText.color = Color.white;
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

            // trigger the vignette effect coroutine
            StartCoroutine(ShowVignetteEffect());

            // update previousMissCounter
            previousMissCounter = currentMisses;

            MissedHit();
        }

        GameManager.instance.player.TakeDamage(2);
        AudioManager.instance.missSFX.Play();

        Debug.Log("Miss");
        Debug.Log("Misses updated. Current misses: " + currentMisses);
    }

    private IEnumerator ShowVignetteEffect()
    {
        intensity = 0.4f;

        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while (intensity > 0)
        {
            intensity -= 0.01f;

            if (intensity < 0) intensity = 0;

            _vignette.intensity.Override(intensity);

            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;
    }
}