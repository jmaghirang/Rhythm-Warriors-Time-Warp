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
        // check if instance already exists
        if (instance == null)
        {
            // if no instance exists, set instance to this
            instance = this;
        }
        else if (instance != this)
        {
            // if instance already exists and it's not this, then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of InventoryUIManager.
            Destroy(gameObject);
        }

        // sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    
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