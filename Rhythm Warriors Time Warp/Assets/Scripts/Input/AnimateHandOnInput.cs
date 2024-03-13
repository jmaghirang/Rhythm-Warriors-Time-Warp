using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// code from: https://youtu.be/8PCNNro7Rt0?si=2BsfvjfReF7gHY6-

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty gripAnimation;

    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
