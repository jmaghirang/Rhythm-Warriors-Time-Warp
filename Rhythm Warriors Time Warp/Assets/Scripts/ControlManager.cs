using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;

    private void Awake()
    {
        instance = this;
    }


    // ------------------------------------------------------------
    // Defining attributes
    // ------------------------------------------------------------


    // Pause menu to trigger
    public GameObject pauseMenu;

    // Input on controller to trigger pause menu
    // Set to menu button on left controller
    // With XR Device Simulator, it is Shift + M
    public InputActionProperty pauseButton;


    // ------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Handling pause button
        // If the pause button was pressed
        if (pauseButton.action.WasPressedThisFrame())
        {
            // Show pause menu
            TriggerPauseMenu();
        }

        // Orient the pause menu to face the player with the correct rotation
        MenuManager.instance.OrientMenu(pauseMenu);
    }

    void TriggerPauseMenu()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        MenuManager.instance.ShowMenu(pauseMenu);
    }
}
