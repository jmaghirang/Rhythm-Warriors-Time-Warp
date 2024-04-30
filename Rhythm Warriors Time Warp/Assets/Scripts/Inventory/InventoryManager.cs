using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public GameObject inventoryUI; // assign inventory ui in the inspector
    private InputActionProperty inventoryButton;
    public InventoryData inventoryData;

    public bool canViewInventory = false;

    private void Awake()
    {
        instance = this;
        LoadInventoryData(); // load inventory data on startup
    }

    public void Update()
    {
        if (ControlManager.instance.inventoryButton.action.WasPressedThisFrame() && canViewInventory)
        {
            ToggleInventory();
        }
    }
    public InventoryData GetInventoryData()
    {
        return inventoryData;
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

    // this method is called when an artifact is collected
    // it accepts an artifact id as a parameter
    public void CollectArtifact(string artifactID)
    {
        // if the artifact id is not in the dictionary, add it and set the value to true
        if (!inventoryData.artifactsCollected.ContainsKey(artifactID))
        {
            inventoryData.artifactsCollected.Add(artifactID, true);
        }
        else // if the artifact id is already in the dictionary, set the value to true
        {
            inventoryData.artifactsCollected[artifactID] = true;
        }
        // save the updated inventory data
        SaveInventoryData();
    }

    // this method checks if an artifact has been collected
    // it accepts an artifact id as a parameter and returns a boolean
    public bool IsArtifactCollected(string artifactID)
    {
        // if the artifact id is in the dictionary, return the value (true if collected, false otherwise)
        if (inventoryData.artifactsCollected.ContainsKey(artifactID))
        {
            return inventoryData.artifactsCollected[artifactID];
        }
        // if the artifact id is not in the dictionary, return false
        return false;
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