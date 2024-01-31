using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// some code referenced from:
// https://developer.oculus.com/documentation/unity/unity-haptics-overview/
// https://www.youtube.com/watch?v=HxqnDww2Fjo

public class HapticFeedback : MonoBehaviour
{
    // references to the left and right hand controllers
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;

    void Start()
    {
        // get a list of input devices
        List<InputDevice> devices = new List<InputDevice>();

        // get the left hand controller
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, devices);
        if (devices.Count > 0)
        {
            leftHandDevice = devices[0];
        }

        // clear the list of devices
        devices.Clear();

        // get the right hand controller
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, devices);
        if (devices.Count > 0)
        {
            rightHandDevice = devices[0];
        }
    }

    // method to send a haptic impulse to a device
    void SendHapticImpulse(InputDevice device, float amplitude, float duration)
    {
        // get the haptic capabilities of the device
        HapticCapabilities capabilities;
        if (!device.TryGetHapticCapabilities(out capabilities))
        {
            Debug.Log("Unable to get haptic capabilities");
            return;
        }

        // if the device supports impulse, send a haptic impulse
        if (capabilities.supportsImpulse)
        {
            uint channel = 0;
            device.SendHapticImpulse(channel, amplitude, duration);
        }
    }

    // method to be called when the player gets hit
    public void PlayerGotHit()
    {
        // send a haptic impulse to both controllers
        SendHapticImpulse(leftHandDevice, 0.5f, 1.0f);
        SendHapticImpulse(rightHandDevice, 0.5f, 1.0f);
    }
}