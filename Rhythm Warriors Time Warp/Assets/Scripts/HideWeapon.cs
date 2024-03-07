using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ControlManager.instance.hideButton.action.WasPressedThisFrame())
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
