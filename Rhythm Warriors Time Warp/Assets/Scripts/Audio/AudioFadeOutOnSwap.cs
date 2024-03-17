using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOutOnSwap : MonoBehaviour
{
    public AudioSource preview;
    public float timeToFade = 1.25f;

    void Start()
    {

    }

    public IEnumerator SwapTrack()
    {
        float initialVolume = preview.volume;

        while (preview.volume > 0)
        {
            preview.volume -= initialVolume * Time.deltaTime / timeToFade;

            yield return null;
        }

        preview.Stop();
        preview.volume = initialVolume;
    }
}
