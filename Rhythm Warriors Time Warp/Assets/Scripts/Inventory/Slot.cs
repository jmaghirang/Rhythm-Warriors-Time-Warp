using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Slot : MonoBehaviour
{
    public XRGrabInteractable ItemInSlot;
    private MeshRenderer slotRenderer;
    private Color originalColor;

    void Start()
    {
        slotRenderer = GetComponent<MeshRenderer>();
        originalColor = slotRenderer.material.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;

        XRGrabInteractable interactable = other.GetComponent<XRGrabInteractable>();

        if (interactable == null) return;

        if (interactable.isSelected)
        {
            InsertItem(interactable);
        }
    }

    void InsertItem(XRGrabInteractable interactable)
    {
        interactable.transform.SetParent(transform);
        interactable.transform.localPosition = Vector3.zero;
        interactable.transform.localRotation = Quaternion.identity;
        ItemInSlot = interactable;
        slotRenderer.material.color = Color.gray;
    }

    public void ResetColor()
    {
        slotRenderer.material.color = originalColor;
    }
}