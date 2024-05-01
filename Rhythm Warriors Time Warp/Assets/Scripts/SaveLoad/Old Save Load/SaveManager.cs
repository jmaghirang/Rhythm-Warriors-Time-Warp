/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private string savePath;

    private void Awake()
    {
        instance = this;
        savePath = Application.persistentDataPath + "/save.json";
    }

    public void SaveGame(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
    }

    public GameData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
}
*/