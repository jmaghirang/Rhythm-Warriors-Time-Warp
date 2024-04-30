using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

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

    public void CollectArtifact(string artifactID)
    {
        if (!inventoryData.artifactsCollected.ContainsKey(artifactID))
        {
            inventoryData.artifactsCollected.Add(artifactID, true);
        }
        else
        {
            inventoryData.artifactsCollected[artifactID] = true;
        }
        SaveInventoryData();
    }

    public bool IsArtifactCollected(string artifactID)
    {
        if (inventoryData.artifactsCollected.ContainsKey(artifactID))
        {
            return inventoryData.artifactsCollected[artifactID];
        }
        return false;
    }
}