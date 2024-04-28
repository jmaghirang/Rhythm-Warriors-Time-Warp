using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class represents the inventory data
[System.Serializable]
public class InventoryData
{
    // a dictionary where the key is the artifact id and the value is a boolean indicating whether it's been collected
    public Dictionary<string, bool> artifactsCollected = new Dictionary<string, bool>();
}