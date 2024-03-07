using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomObjectGrabber : MonoBehaviour
{
    private GameObject grabbedObject;
    private bool isGrabbing = false;

    private void Update()
    {
        // Check if the primary button (usually index trigger) is pressed
        if (IsButtonPressed(XRControllerButton.PrimaryButton))
        {
            if (!isGrabbing)
            {
                // Try to grab an object if not already grabbing
                TryGrabObject();
            }
        }
        else
        {
            // Release the grabbed object if the button is not pressed
            ReleaseObject();
        }
    }

    private void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            grabbedObject = hit.collider.gameObject;

            // Check if the object is grabbable (you may have a specific tag or component for this)
            if (grabbedObject.CompareTag("Grabbable"))
            {
                // Set up the object as a child of the controller
                grabbedObject.transform.SetParent(transform);
                isGrabbing = true;
                Debug.Log("Grabbed object: " + grabbedObject.name);
            }
        }
    }

    private void ReleaseObject()
    {
        if (isGrabbing)
        {
            // Release the grabbed object
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            isGrabbing = false;
            Debug.Log("Released the grabbed object.");
        }
    }

    private bool IsButtonPressed(XRControllerButton button)
    {
        XRController controller = GetComponent<XRController>();
        if (controller != null)
        {
            InputHelpers.Button handButton = InputHelpers.Button.PrimaryButton;

            switch (button)
            {
                case XRControllerButton.PrimaryButton:
                    handButton = InputHelpers.Button.PrimaryButton;
                    break;
                // Add more cases for other buttons if needed
            }

            bool isPressed = controller.inputDevice.IsPressed(handButton, out _);
            Debug.Log("Button " + handButton.ToString() + " pressed: " + isPressed);

            return isPressed;
        }

        return false;
    }
}

public enum XRControllerButton
{
    PrimaryButton
    // Add more button enums as needed
}