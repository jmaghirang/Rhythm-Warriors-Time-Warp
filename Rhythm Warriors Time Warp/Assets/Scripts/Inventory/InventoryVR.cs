using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor;
    bool UIActive;

    private void Start()
    {
        Inventory.SetActive(false);
        UIActive = false;
    }

    private void Update()
    {
        InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonState) && primaryButtonState)
        {
            UIActive = !UIActive;
            Inventory.SetActive(UIActive);

            if (UIActive)
            {
                XRGrabInteractable[] interactables = Inventory.GetComponentsInChildren<XRGrabInteractable>();
                
                foreach (var interactable in interactables)
                {
                    // Make the object interactable or non-interactable based on UI state
                    interactable.interactionLayerMask = UIActive ? LayerMask.GetMask("UI") : LayerMask.GetMask("Default");
                }
            }
        }

        if (UIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }
}
