using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : ProgressBar
{
    void Start()
    {
        max = SongManager.instance.audioSource.clip.length;
    }

    void Update()
    {
        current = SongManager.GetAudioSourceTime();
        GetCurrentFill();
    }
}
