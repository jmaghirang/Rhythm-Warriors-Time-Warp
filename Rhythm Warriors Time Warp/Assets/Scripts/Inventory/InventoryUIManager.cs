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
        // for each artifact id in the inventory data, update the material of the corresponding renderer
        foreach (var artifactID in InventoryManager.instance.inventoryData.artifactsCollected.Keys)
        {
            UpdateArtifactMaterial(artifactID, InventoryManager.instance.IsArtifactCollected(artifactID));
        }
    }

    // this method updates the material of an artifact renderer
    // it accepts an artifact id and a boolean indicating whether the artifact has been collected
    public void UpdateArtifactMaterial(string artifactID, bool isCollected)
    {
        // if the artifact id is in the dictionary, update the material of the corresponding renderer
        if (artifactRenderers.ContainsKey(artifactID))
        {
            artifactRenderers[artifactID].material = isCollected ? collectedMaterial : notCollectedMaterial;
        }
    }
}