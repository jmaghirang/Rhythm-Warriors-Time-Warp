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

    // Distance menu will spawn from player's head
    public float spawnDistance = 0.5f;

    public GameObject pauseMenu;

    /* add these when they're added in the settings menu
    public GameObject customizationSection; // reference to the customization section in the pause menu???
    public UIToggle uiToggle; // reference to the UIToggle component, add when the toggle is added in settings
    private bool customizationMode = false; // flag to indicate if the customization mode is enabled
    public Toggle edgeBlurToggle; // reference to the edge blur toggle element
    public Toggle motionBlurToggle; // reference to the motion blur toggle element
    public PostProcessVolume postProcessVolume; // reference to the post processing volume component
    */

    // Start is called before the first frame update
    void Start()
    {
        /* load player preferences for edge blur, motion blur, and UI toggle
        edgeBlurToggle.isOn = PlayerPrefs.GetInt("EdgeBlurEnabled", 1) == 1;
        motionBlurToggle.isOn = PlayerPrefs.GetInt("MotionBlurEnabled", 1) == 1;
        uiToggleToggle.isOn = PlayerPrefs.GetInt("UIToggleEnabled", 1) == 1;

        // apply the initial settings for edge blur, motion blur, and UI toggle
        ToggleEdgeBlur(edgeBlurToggle.isOn);
        ToggleMotionBlur(motionBlurToggle.isOn);
        ToggleUIToggle(uiToggleToggle.isOn);
        */
    }

    // Update is called once per frame
    void Update()
    {
        OrientMenu(pauseMenu);
    }

    public void ShowMenu(GameObject m)
    {
        // Position of menu will be spawned at the position of where the player is looking
        m.transform.position = playerCamera.position + new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * spawnDistance;
    }

    public void OrientMenu(GameObject m)
    {
        // Menu will always face the position of the player's head
        m.transform.LookAt(new Vector3(playerCamera.position.x, m.transform.position.y, playerCamera.transform.position.z));

        // Menu is at the right orientation
        m.transform.forward *= -1;
    }

    public void TriggerPauseMenu()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        ShowMenu(pauseMenu);
    }

    /* add when the UI and motion blur toggles are added in settings
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

    public void ToggleUIToggle(bool isEnabled)
    {
        PlayerPrefs.SetInt("UIToggleEnabled", isEnabled ? 1 : 0);
        // apply UI toggle setting to the game
        // code to enable/disable UI toggle
        uiToggle.gameObject.SetActive(isEnabled);
    }
    */
}