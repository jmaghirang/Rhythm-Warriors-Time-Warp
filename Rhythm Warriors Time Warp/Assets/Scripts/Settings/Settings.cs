using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
//using NovaSamples.UIControls;

public class Settings : MonoBehaviour
{
    public XROrigin player;

    private TeleportationProvider teleMove;
    private ActionBasedContinuousMoveProvider contMove;
    private ActionBasedSnapTurnProvider snapRotation;
    private ActionBasedContinuousTurnProvider contRotation;

    public Slider volume;

    public Offset offset;

    public CameraShake cameraShake;
    public HapticFeedback hapticFeedback;
    public TunnelingVignetteController edgeBlur;
    public Volume damageEffect; // reference to the post processing volume component

    public Toggle teleMoveToggle;
    public Toggle contMoveToggle;
    public Toggle snapRotationToggle;
    public Toggle contRotationToggle;

    public Toggle hapticFeedbackToggle;

    public Toggle cameraShakeToggle;
    public Toggle edgeBlurToggle;
    public Toggle damageEffectToggle;

    public Toggle panelsToggle; // toggle for both the score and miss panels
    //public GameObject scorePanel; // reference to the score panel
    //public GameObject missPanel; // reference to the miss panel

    /* 
    // add these when they're added in the settings menu
    public GameObject customizationSection; // reference to the customization section in the pause menu???
    private bool customizationMode = false; // flag to indicate if the customization mode is enabled
    */

    // Start is called before the first frame update
    void Start()
    {
        //teleMove = player.GetComponent<TeleportationProvider>();
        contMove = player.GetComponent<ActionBasedContinuousMoveProvider>();
        snapRotation = player.GetComponent<ActionBasedSnapTurnProvider>();
        contRotation = player.GetComponent<ActionBasedContinuousTurnProvider>();

        contMoveToggle.isOn = PlayerPrefs.GetInt("ContinuousMovementEnabled", 1) == 1;
        teleMoveToggle.isOn = PlayerPrefs.GetInt("TeleportationEnabled", 0) == 1;
        contRotationToggle.isOn = PlayerPrefs.GetInt("ContinuousRotationEnabled", 0) == 1;
        snapRotationToggle.isOn = PlayerPrefs.GetInt("SnapRotationEnabled", 1) == 1;

        volume.value = PlayerPrefs.GetFloat("MasterVolume", 1);

        offset.offsetValue = PlayerPrefs.GetInt("Offset", 0);

        cameraShakeToggle.isOn = PlayerPrefs.GetInt("CameraShakeEnabled", 1) == 1;
        edgeBlurToggle.isOn = PlayerPrefs.GetInt("EdgeBlurEnabled", 1) == 1;
        damageEffectToggle.isOn = PlayerPrefs.GetInt("DamageEffectEnabled", 1) == 1;

        hapticFeedbackToggle.isOn = PlayerPrefs.GetInt("HapticFeedbackEnabled", 1) == 1;

        panelsToggle.isOn = PlayerPrefs.GetInt("PanelsEnabled", 1) == 1;

        /*
        // apply the initial settings for edge blur, motion blur, and UI toggle
        ToggleEdgeBlur(edgeBlurToggle.isOn);
        */
    }

    // Update is called once per frame
    void Update()
    {
        ToggleRotation(snapRotationToggle.isOn);

        ToggleCameraShake(cameraShakeToggle.isOn);
        ToggleHapticFeedback(hapticFeedbackToggle.isOn);
        ToggleEdgeBlur(edgeBlurToggle.isOn);
        ToggleDamageEffect(damageEffectToggle.isOn);

        SetVolume();

        SetOffset();

        TogglePanels(panelsToggle.isOn);
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
        else if (snapRotationToggle.isOn)
        {
            snapRotation.enabled = true;
            contRotation.enabled = false;
        }
    }

    public void ToggleMovement(bool defaultSetting)
    {
        // Continuous will be on by default
        PlayerPrefs.SetInt("ContinuousMovementEnabled", defaultSetting ? 1 : 0);
        PlayerPrefs.SetInt("TeleportationEnabled", defaultSetting ? 0 : 1);

        if (teleMoveToggle.isOn)
        {
            teleMove.enabled = true;
            contMove.enabled = false;
        }
        else if (contMoveToggle.isOn)
        {
            contMove.enabled = true;
            teleMove.enabled = false;
        }
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", volume.value);
    }

    public void SetOffset()
    {
        PlayerPrefs.SetInt("Offset", offset.offsetValue);
    }

    public void ToggleCameraShake(bool defaultSetting)
    {
        PlayerPrefs.SetInt("CameraShakeEnabled", defaultSetting ? 1 : 0);
        // apply camera shake setting to the game
        // code to enable/disable camera shake

        if (cameraShakeToggle.isOn)
        {
            cameraShake.enabled = true;
        }
        else
        {
            cameraShake.enabled = false;
        }
    }

    public void ToggleHapticFeedback(bool defaultSetting)
    {
        PlayerPrefs.SetInt("HapticFeedbackEnabled", defaultSetting ? 1 : 0);
        // apply haptic feedback setting to the game
        // code to enable/disable haptic feedback

        if (hapticFeedbackToggle.isOn)
        {
            hapticFeedback.enabled = true;
        }
        else
        {
            hapticFeedback.enabled = false;
        }
    }

    public void ToggleEdgeBlur(bool defaultSetting)
    {
        PlayerPrefs.SetInt("EdgeBlurEnabled", defaultSetting ? 1 : 0);

        if (edgeBlurToggle.isOn)
        {
            edgeBlur.enabled = true;
        }
        else
        {
            edgeBlur.enabled = false;
        }
    }

    public void ToggleDamageEffect(bool defaultSetting)
    {
        PlayerPrefs.SetInt("DamageEffect", defaultSetting ? 1 : 0);

        if (damageEffectToggle.isOn)
        {
            damageEffect.enabled = true;
        }
        else
        {
            damageEffect.enabled = false;
        }
    }

    public void TogglePanels(bool isEnabled)
    {
        // save the setting
        PlayerPrefs.SetInt("PanelsEnabled", isEnabled ? 1 : 0);
    }

    /* 
    // add when the motion blur toggles are added in settings
    public void ToggleEdgeBlur(bool isEnabled)
    {
        PlayerPrefs.SetInt("EdgeBlurEnabled", isEnabled ? 1 : 0);
        // apply edge blur setting to the game
        // code to enable/disable edge blur
    }
    */
}
