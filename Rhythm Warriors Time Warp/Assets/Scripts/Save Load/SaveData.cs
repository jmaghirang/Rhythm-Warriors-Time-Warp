using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public string playerName;
    public string[] collectedFragmentIDs;
    public int currentLevel;
    public string dateSaved;
    public string timeSaved;
}