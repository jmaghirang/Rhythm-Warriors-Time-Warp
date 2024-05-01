using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public DateTime dateSaved;
    public DateTime saveTime;
    public string[] collectedFragmentIDs;
    public int currentLevel;
}