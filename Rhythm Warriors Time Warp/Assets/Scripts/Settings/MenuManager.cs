using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code referenced from
// How to Make a VR Game in Unity 2022 - PART 7 - User Interface
// Valem Tutorials

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        instance = this;
    }


    // Position rotation of player's head
    public Transform playerCamera;

    // Pause menu
    public Menu pauseMenu;

    // NPC and Player 
    private NPC n;
    private Player p;

    /* 
    // add these when they're added in the settings menu
    public GameObject customizationSection; // reference to the customization section in the pause menu???
    private bool customizationMode = false; // flag to indicate if the customization mode is enabled

    public UIToggle uiToggle; // toggle for the UI toggle (score, misses, health bar, progress)
    public Toggle edgeBlurToggle; // toggle for the edge blur
    public Toggle motionBlurToggle; // toggle for the motion blur
    public PostProcessVolume postProcessVolume; // reference to the post processing volume component
    public Toggle cameraShakeToggle; // toggle for camera shake
    public Toggle hapticFeedbackToggle; // toggle for haptic feedback
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
        OrientMenu(pauseMenu);
    }

    public void ShowMenu(Menu m)
    {
        // Position of menu will be spawned at the position of where the player is looking
        // Spawn distance is distance menu will spawn from player's head
        m.UI.transform.position = playerCamera.position + new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * m.spawnDistance;
    }

    public void OrientMenu(Menu m)
    {
        // Menu will always face the position of the player's head
        m.UI.transform.LookAt(new Vector3(playerCamera.position.x, m.UI.transform.position.y, playerCamera.transform.position.z));

        // Menu is at the right orientation
        m.UI.transform.forward *= -1;
    }

    public void ShowDialogue(Menu m)
    {
        n = DialogueManager.instance.npc;
        p = GameManager.instance.player;

        // Position of "menu" will be spawned a position depending on which character is speaking
        // Concerning dialogue
        if (n.isSpeaking && !p.isSpeaking)
        {
            // Position of menu will be spawned in front of the npc speaking and is faced towards the player
            m.UI.transform.position = n.transform.position + new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * -(m.spawnDistance + 0.5f);
        }
        else
        {
            if (!n.isSpeaking && p.isSpeaking)
            {
                ShowMenu(m);
            }
        }
    }

    public void TriggerPauseMenu()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        pauseMenu.UI.SetActive(!pauseMenu.UI.activeSelf);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        ShowMenu(pauseMenu);
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