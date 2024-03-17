using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventoryUI; // assign inventory ui in the inspector

    private InputActionProperty inventoryButton;

    private void Awake()
    {
        instance = this;
    }

    public void SetInventoryButton(InputActionProperty button)
    {
        // unregister the previous action if any
        if (inventoryButton != null)
        {
            inventoryButton.action.performed -= _ => ToggleInventory();
        }

        // set the new button and register the action
        inventoryButton = button;
        inventoryButton.action.performed += _ => ToggleInventory();
    }

    public void ToggleInventory()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void OnDestroy()
    {
        // unregister the inventory button action
        if (inventoryButton != null)
        {
            inventoryButton.action.performed -= _ => ToggleInventory();
        }
    }
}
