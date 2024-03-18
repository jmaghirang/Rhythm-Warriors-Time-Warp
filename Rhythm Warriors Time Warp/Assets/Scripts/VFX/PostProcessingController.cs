using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    public void StartVignetteEffect()
    {
        // get the vignette effect
        Vignette vignette = postProcessVolume.profile.GetSetting<Vignette>();

        // enable the vignette effect and set the intensity to 1 for a full screen flash
        vignette.active = true;
        vignette.intensity.value = 1;

        // start a coroutine to gradually reduce the intensity and then disable the Vignette effect
        StartCoroutine(ReduceVignetteIntensity(vignette));
    }

    public IEnumerator ReduceVignetteIntensity(Vignette vignette)
    {
        while (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= Time.deltaTime; // reduce intensity over time
            yield return null; // wait for the next frame
        }
        vignette.active = false; // disable the vignette effect
    }
}