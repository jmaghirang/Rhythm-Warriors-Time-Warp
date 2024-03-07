using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageEffect : MonoBehaviour
{
    public PostProcessVolume damagePostProcessVolume;

    void Start()
    {
        // ensure the post-processing volume is disabled initially
        damagePostProcessVolume.enabled = false;
    }

    // call this function when the player takes damage
    public void ApplyDamageEffect()
    {
        // enable the post-processing volume to activate the red edge effect
        damagePostProcessVolume.enabled = true;

        StartCoroutine(DisableDamageEffectAfterDelay());
    }

    // coroutine to disable the effect after a delay
    IEnumerator DisableDamageEffectAfterDelay()
    {
        yield return new WaitForSeconds(2f); // duration

        // disable the post-processing volume to deactivate the red edge effect
        damagePostProcessVolume.enabled = false;
    }
}
