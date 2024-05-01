using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fragment : MonoBehaviour
{
    public int ID;
    public bool isCollected = false;
    public bool grabbable = false;

    void Start()
    {
        if (grabbable)
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.selectEntered.AddListener(x => FragmentCollected());
            }
            else
            {
                Debug.LogWarning("XRGrabInteractable component not found on the object.");
            }
        }
    }

    void FragmentCollected()
    {
        isCollected = true;
        //InventoryManager.instance.CollectArtifact(ID.ToString());
        //InventoryUIManager.instance.UpdateArtifactMaterial(ID.ToString(), isCollected);
    }
}