using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveGameManager : MonoBehaviour
{
    public static SaveGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(string playerName, string[] collectedFragmentIDs, int currentLevel)
    {
        SaveData saveData = new SaveData();
        saveData.playerName = playerName;
        saveData.collectedFragmentIDs = collectedFragmentIDs;
        saveData.currentLevel = currentLevel;
        saveData.dateSaved = DateTime.Today.ToShortDateString();
        saveData.timeSaved = DateTime.Now.ToShortTimeString();

        string saveJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", saveJson);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        string saveJson = PlayerPrefs.GetString("SaveData");
        if (!string.IsNullOrEmpty(saveJson))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(saveJson);

            // call GameManager to load data
            GameManager.instance.LoadPlayerProgress(saveData);

            // load collected fragments
            foreach (string fragmentID in saveData.collectedFragmentIDs)
            {
                InventoryManager.instance.CollectArtifact(fragmentID);
            }

            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }
}