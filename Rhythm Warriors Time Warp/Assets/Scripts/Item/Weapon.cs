using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : Item
{
    public bool isEquipped;
    public Transform controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip()
    {
        transform.SetParent(controller);
        // Remove rigidbody? movement when picked up is odd
    }
}
