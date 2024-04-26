using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventoryUI; // assign inventory ui in the inspector
    private InputActionProperty inventoryButton;
    private InventoryData inventoryData;

    private void Awake()
    {
        instance = this;
        LoadInventoryData(); // load inventory data on startup
    }

    private void LoadInventoryData()
    {
        // load inventory data from persistent storage
        string jsonData = PlayerPrefs.GetString("InventoryData");
        inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);
        if (inventoryData == null)
        {
            inventoryData = new InventoryData();
            SaveInventoryData();
        }
    }

    private void SaveInventoryData()
    {
        // save inventory data to persistent storage
        string jsonData = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString("InventoryData", jsonData);
        PlayerPrefs.Save();
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

    public void CollectArtifact()
    {
        inventoryData.isArtifactCollected = true;
        SaveInventoryData();
    }

    public bool IsArtifactCollected()
    {
        return inventoryData.isArtifactCollected;
    }

    /* add back in when we have a button to toggle inventory
    void OnDestroy()
    {
        // unregister the inventory button action
        if (inventoryButton != null)
        {
            inventoryButton.action.performed -= _ => ToggleInventory();
        }
    }
    */
}