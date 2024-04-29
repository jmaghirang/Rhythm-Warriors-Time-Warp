using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;

// some VFX code referenced from: https://www.youtube.com/watch?v=N3JR5m7knGQ&t=86s
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float intensity = 0; // intensity of the vignette effect

    PostProcessVolume _volume;
    Vignette _vignette;

    // reference to other scripts
    public CameraShake cameraShake;
    public HapticFeedback hapticFeedback;

    private int currentScore = 0;
    private int currentMisses = 0;
    private int previousMissCounter = 0; // previous value of missCounter

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _volume = gameObject.GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings(out _vignette);

        if(!_vignette)
        {
            previousMissCounter("error, vignette empty")
        }
        else
        {
            _vignette.enabled.Override(false);
        }
    }

    void Update()
    {

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

            _vignette.intensity.Override(intensity):

            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;
    }
}