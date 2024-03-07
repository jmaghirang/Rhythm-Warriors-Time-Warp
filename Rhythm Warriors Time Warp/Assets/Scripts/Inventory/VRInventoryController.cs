using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRInventoryController : MonoBehaviour
{
    public XRController leftController;
    public GameObject inventoryUI;

    private bool isInventoryOpen = false;

    private void Start()
    {
        leftController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed);
        leftController.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactor is XRController)
        {
            // toggle inventory UI on Y button press
            if (args.interactor.selectAction.action.activeControl.name == "YButton")
            {
                ToggleInventoryUI();
            }
        }
    }

    private void ToggleInventoryUI()
    {
        isInventoryOpen = !isInventoryOpen;

        // activate/deactivate the inventory UI based on the button press
        inventoryUI.SetActive(isInventoryOpen);
    }
}
