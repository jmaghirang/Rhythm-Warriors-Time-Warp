using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    // public PostProcessVolume postProcessVolume; // reference to the post-processing Volume
    // private Vignette vignette; // reference to the vignette effect

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

    /*private void Start()
    {
        // get the vignette effect from the post-processing volume if available
        if (postProcessVolume != null && postProcessVolume.profile != null)
        {
            postProcessVolume.profile.TryGetSettings(out vignette);
        }
    }*/

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

            // update previousMissCounter
            previousMissCounter = currentMisses;

            /*if (vignette != null)
            {
                vignette.intensity.value = 1f; // set vignette intensity to maximum
                StartCoroutine(ResetVignette());
            }*/
        }

        GameManager.instance.player.TakeDamage(2);
        AudioManager.instance.missSFX.Play();

        Debug.Log("Miss");
        Debug.Log("Misses updated. Current misses: " + currentMisses);
    }

    /*private IEnumerator ResetVignette()
    {
        yield return new WaitForSeconds(0.1f); // adjust the duration as needed
        vignette.intensity.value = 0f; // reset vignette intensity
    }*/
}
