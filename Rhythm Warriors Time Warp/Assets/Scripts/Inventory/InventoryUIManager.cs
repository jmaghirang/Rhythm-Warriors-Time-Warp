using UnityEngine;
using System.Collections.Generic;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public Material collectedMaterial; // assign the "Runes_Stones_Material" in the inspector
    public Material notCollectedMaterial; // assign the "Runes_Texture" in the inspector
    public Dictionary<string, Renderer> artifactRenderers; // assign the Renderer of each artifact in the inventory UI in the inspector

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    
        foreach (var artifactID in InventoryManager.instance.inventoryData.artifactsCollected.Keys)
        {
            UpdateArtifactMaterial(artifactID, InventoryManager.instance.IsArtifactCollected(artifactID));
        }
    }

    public void UpdateArtifactMaterial(string artifactID, bool isCollected)
    {
        if (artifactRenderers.ContainsKey(artifactID))
        {
            artifactRenderers[artifactID].material = isCollected ? collectedMaterial : notCollectedMaterial;
        }
    }

    // add this method to update the UI when a new artifact is collected
    public void OnArtifactCollected(string artifactID)
    {
        if (!artifactRenderers.ContainsKey(artifactID))
        {
            Debug.LogError("Artifact ID not found in artifactRenderers dictionary.");
            return;
        }

        bool isCollected = InventoryManager.instance.IsArtifactCollected(artifactID);
        artifactRenderers[artifactID].material = isCollected ? collectedMaterial : notCollectedMaterial;
    }
}