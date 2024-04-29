using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fragment : MonoBehaviour
{
    public int ID;
    public bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(x => FragmentCollected());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FragmentCollected()
    {
        isCollected = true;
        InventoryUIManager.instance.UpdateArtifactMaterial(ID.ToString(), isCollected);
    }
}

// Jamie Lee

/*
public class Fragment : MonoBehaviour
{
    public int ID;
    public bool isCollected = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(x => FragmentCollected());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FragmentCollected()
    {
        isCollected = true;
        InventoryUIManager.instance.UpdateArtifactMaterial(isCollected);
    }
}
*/