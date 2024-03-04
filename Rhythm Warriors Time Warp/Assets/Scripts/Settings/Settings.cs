using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Settings : MonoBehaviour
{
    public TeleportationProvider teleMove;
    public ActionBasedContinuousMoveProvider contMove;

    public ActionBasedSnapTurnProvider snapRotation;
    public ActionBasedContinuousTurnProvider contRotation;

    public CameraShake camShake;

    public HapticFeedback hapticFeedback;

    public Toggle teleMoveToggle;
    public Toggle contMoveToggle;
    public Toggle snapRotationToggle;
    public Toggle contRotationToggle;

    /*
    public Toggle camShakeToggle;
    public Toggle hapticFeedbackToggle;
    public Toggle edgeBlurToggle;
    public Toggle motionBlurToggle;
    */

    public UIToggle uiToggle; // toggle for the UI toggle (score, misses, health bar, progress)

    public PostProcessVolume postProcessVolume; // reference to the post processing volume component

    /* 
    // add these when they're added in the settings menu
    public GameObject customizationSection; // reference to the customization section in the pause menu???
    private bool customizationMode = false; // flag to indicate if the customization mode is enabled
    */

    // Start is called before the first frame update
    void Start()
    {
        /* load player preferences
         uiToggleToggle.isOn = PlayerPrefs.GetInt("UIToggleEnabled", 1) == 1;
         edgeBlurToggle.isOn = PlayerPrefs.GetInt("EdgeBlurEnabled", 1) == 1;
         motionBlurToggle.isOn = PlayerPrefs.GetInt("MotionBlurEnabled", 1) == 1;
         cameraShakeToggle.isOn = PlayerPrefs.GetInt("CameraShakeEnabled", 1) == 1;
         hapticFeedbackToggle.isOn = PlayerPrefs.GetInt("HapticFeedbackEnabled", 1) == 1;
    
         // apply the initial settings for edge blur, motion blur, and UI toggle
         ToggleUIToggle(uiToggleToggle.isOn);
         ToggleEdgeBlur(edgeBlurToggle.isOn);
         ToggleMotionBlur(motionBlurToggle.isOn);
         ToggleCameraShake(cameraShakeToggle.isOn);
         ToggleHapticFeedback(hapticFeedbackToggle.isOn);
         */
    }

    // Update is called once per frame
    void Update()
    {
        ToggleRotation(snapRotationToggle.isOn);
    }

    public void ToggleRotation(bool defaultSetting)
    {
        // Snap will be on by default
        PlayerPrefs.SetInt("SnapRotationEnabled", defaultSetting ? 1 : 0);
        PlayerPrefs.SetInt("ContinuousRotationEnabled", defaultSetting ? 0 : 1);

        if (contRotationToggle.isOn)
        {
            contRotation.enabled = true;
            snapRotation.enabled = false;
        }
        else
        {
            if (snapRotationToggle.isOn)
            {
                snapRotation.enabled = true;
                contRotation.enabled = false;
            }
        }
    }

    /* 
    // add when the UI and motion blur toggles are added in settings

    public void ToggleUIToggle(bool isEnabled)
    {
        PlayerPrefs.SetInt("UIToggleEnabled", isEnabled ? 1 : 0);
        // apply UI toggle setting to the game
        // code to enable/disable UI toggle
        uiToggle.gameObject.SetActive(isEnabled);
    }

    public void ToggleEdgeBlur(bool isEnabled)
    {
        PlayerPrefs.SetInt("EdgeBlurEnabled", isEnabled ? 1 : 0);
        // apply edge blur setting to the game
        // code to enable/disable edge blur
    }

    public void ToggleMotionBlur(bool isEnabled)
    {
        PlayerPrefs.SetInt("MotionBlurEnabled", isEnabled ? 1 : 0);
        // apply motion blur setting to the game
        // code to enable/disable motion blur
        postProcessVolume.profile.motionBlur.enabled = isEnabled;
    }

    public void ToggleCameraShake(bool isEnabled)
    {
        PlayerPrefs.SetInt("CameraShakeEnabled", isEnabled ? 1 : 0);
        // apply camera shake setting to the game
        // code to enable/disable camera shake
    }

    public void ToggleHapticFeedback(bool isEnabled)
    {
        PlayerPrefs.SetInt("HapticFeedbackEnabled", isEnabled ? 1 : 0);
        // apply haptic feedback setting to the game
        // code to enable/disable haptic feedback
    }
    */
}