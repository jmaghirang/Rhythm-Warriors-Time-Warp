using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    private DepthOfField depthOfField;
    private MotionBlur motionBlur;

    private void Start()
    {
        // get the depth of field and motion blur effects from the post processing volume
        postProcessVolume.profile.TryGetSettings(out depthOfField);
        postProcessVolume.profile.TryGetSettings(out motionBlur);
    }
}