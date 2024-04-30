using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fragment : MonoBehaviour
{
    public int ID;
    public bool isCollected = false;

    void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(x => FragmentCollected());
    }

    void FragmentCollected()
    {
        isCollected = true;
        InventoryManager.instance.CollectArtifact(ID.ToString());
        InventoryUIManager.instance.UpdateArtifactMaterial(ID.ToString(), isCollected);
    }
}