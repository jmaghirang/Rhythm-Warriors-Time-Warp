using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        slotImage = GetComponent<Image>();
        originalColor = slotImage.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (ItemInSlot != null) return;

        GameObject obj = other.gameObject;
        if (!IsItem(obj)) return;

        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonState) && triggerButtonState)
        {
            InsertItem(obj);
        }
    }

    bool IsItem(GameObject obj)
    {
        return obj.GetComponent<Item>();
    }

    void InsertItem(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(gameObject.transform, true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = obj.GetComponent<Item>().slotRotation;
        obj.GetComponent<Item>().inSlot = true;
        obj.GetComponent<Item>().currentSlot = this;
        ItemInSlot = obj;
        slotImage.color = Color.gray;
    }

    public void ResetColor()
    {
        slotImage.color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
