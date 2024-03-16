using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    // Variable declaration
    public AudioMixer mixer;

    void Start()
    {
        
    }

    // When the slider is changed apply the volume change
    public void SetLevel(float val)
    {
        // Master volume
        mixer.SetFloat("Volume", Mathf.Log(val) * 20);
    }
}