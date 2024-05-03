using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// some VFX code referenced from: https://www.youtube.com/watch?v=N3JR5m7knGQ&t=86s
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Settings settings;

    public float intensity = 0.5f; // intensity of the vignette effect
    public float duration = 0.5f;

    public Volume _volume;
    private Vignette _vignette;

    // reference to other scripts
    public CameraShake cameraShake;
    public HapticFeedback hapticFeedback;

    public TextMeshProUGUI accuracyText;

    private int currentScore = 0;
    private int currentMisses = 0;
    private int previousMissCounter = 0; // previous value of missCounter

    public GameObject scorePanel; // reference to the score panel
    public GameObject missPanel; // reference to the miss panel

    public int damageTaken; //inspector

    bool isEnabled = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //_volume = gameObject.GetComponent<PostProcessVolume>();
        /*_volume.profile.TryGetSettings(out _vignette);

        if(!_vignette)
        {
            print("error, vignette empty");
        }

        else
        {
            _vignette.enabled.Override(false);
        }*/

        if (_volume.profile.TryGet(out Vignette vignette))
        {
            _vignette = vignette;
        }

        accuracyText.text = "";
    }

    void Update()
    {
        if (!SongManager.instance.audioSource.isPlaying)
        {
            accuracyText.text = "";
        }

        bool showPanels = settings.panelsToggle.isOn;
        
        if (showPanels == true)
        {
            isEnabled = true;
        }
        else if (showPanels == false)
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
            StartCoroutine(FlashVignetteEffect());

            // update previousMissCounter
            previousMissCounter = currentMisses;

            MissedHit();
        }

        GameManager.instance.player.TakeDamage(damageTaken);
        AudioManager.instance.missSFX.Play();

        Debug.Log("Miss");
        Debug.Log("Misses updated. Current misses: " + currentMisses);
    }

    // https://youtu.be/8S22Qt_-nY8?si=sF-Ht-xcSZg1ggAA
    public IEnumerator FlashVignetteEffect()
    {
        FadeIn();

        yield return new WaitForSeconds(.5f);

        FadeOut();
    }

    public void FadeIn()
    {
        StartCoroutine(ShowVignetteEffect(0, intensity));
    }

    public void FadeOut()
    {
        StartCoroutine(ShowVignetteEffect(intensity, 0));
    }

    private IEnumerator ShowVignetteEffect(float startValue, float endValue)
    {
        /* intensity = 0.4f;

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
         yield break;*/

        float elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            float blend = elapsedTime / duration;
            elapsedTime += Time.deltaTime;

            float intensity = Mathf.Lerp(startValue, endValue, blend);
            ApplyValue(intensity);

            yield return null;
        }
    }

    private void ApplyValue(float value)
    {
        _vignette.intensity.Override(value);
    }
}