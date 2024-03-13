using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Input on controller to trigger pause menu
    // Set to menu button on left controller
    // With XR Device Simulator, it is Shift + M
    public InputActionProperty pauseButton;

    // Input on controller to continue dialogue
    // Set to primary button [X] on left controller
    // With XR Device Simulator, it is Shift + B
    public InputActionProperty continueButton;

    // XR Device Simulator, Space + B
    public InputActionProperty hideButton;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}
