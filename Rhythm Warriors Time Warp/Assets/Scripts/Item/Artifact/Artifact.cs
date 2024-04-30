using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public static Artifact instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public Dictionary<int, Fragment> fragments = new()
    {
        { 1, new Fragment {ID=1, isCollected=false} },
        { 2, new Fragment {ID=2, isCollected=false} },
        { 3, new Fragment {ID=3, isCollected=false} },
        { 4, new Fragment {ID=4, isCollected=false} },
        { 5, new Fragment {ID=5, isCollected=false} }
    };
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Artifact Start method called.");
        Debug.Log("InventoryUIManager.instance: " + InventoryUIManager.instance); // check if instance is null
        if (InventoryUIManager.instance != null)
        {
            foreach (var fragment in fragments.Values)
            {
                Debug.Log("Fragment ID: " + fragment.ID + ", isCollected: " + fragment.isCollected);
                InventoryUIManager.instance.UpdateArtifactMaterial(fragment.ID.ToString(), fragment.isCollected);
            }
        }
        else
        {
            Debug.LogError("InventoryUIManager.instance is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*
public class Artifact : MonoBehaviour
{
    public static Artifact instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public Dictionary<int, Fragment> fragments = new()
    {
        { 1, new Fragment {ID=1, isCollected=false} },
        { 2, new Fragment {ID=2, isCollected=false} },
        { 3, new Fragment {ID=3, isCollected=false} },
        { 4, new Fragment {ID=4, isCollected=false} },
        { 5, new Fragment {ID=5, isCollected=false} }
    };
    // Start is called before the first frame update
    void Start()
    {
        // reset the material of the artifact in the inventory UI
        InventoryUIManager.instance.UpdateArtifactMaterial(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
*/