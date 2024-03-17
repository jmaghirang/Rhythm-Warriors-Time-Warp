using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AudioFadeInOnSwap : MonoBehaviour
{
    public AudioSource preview;
    public float timeToFade = 1.25f;

    void Start()
    {

    }

    public IEnumerator StartTrack()
    {
        float initialVolume = 0.2f;
        preview.volume = 0;

        preview.Play();

        while (preview.volume < 1f)
        {
            preview.volume += initialVolume * Time.deltaTime / timeToFade;

            yield return null;
        }

        preview.volume = 1f;
    }
}
