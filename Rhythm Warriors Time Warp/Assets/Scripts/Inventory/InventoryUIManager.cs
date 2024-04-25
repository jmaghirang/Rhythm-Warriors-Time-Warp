using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public Material collectedMaterial; // assign the "Runes_Stones_Material" in the inspector
    public Material notCollectedMaterial; // assign the "Runes_Texture" in the inspector
    public Renderer artifactRenderer; // assign the Renderer of the artifact in the inventory UI in the inspector

    private void Awake()
    {
        instance = this;
        UpdateArtifactMaterial(InventoryManager.instance.IsArtifactCollected());
    }

    public void UpdateArtifactMaterial(bool isCollected)
    {
        artifactRenderer.material = isCollected ? collectedMaterial : notCollectedMaterial;
    }
}