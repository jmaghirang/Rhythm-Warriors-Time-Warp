using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    // Variable declaration
    public AudioMixer mixer;

    // When the slider is changed apply the volume change
    public void setLevel(float val)
    {
        mixer.SetFloat("Volume", val);

    }
}