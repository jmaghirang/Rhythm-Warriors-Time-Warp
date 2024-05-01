using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveLoadUI : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public GameObject[] fragmentPrefabs;
    public Text playerNameText;
    public Text dateSavedText;
    public Text saveTimeText;
    public Text currentLevelText;

    private SaveData currentSaveData;

    void Start()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        LoadSavedData();
    }

    void SaveGame()
    {
        currentSaveData = new SaveData();
        currentSaveData.playerName = "Player";
        currentSaveData.dateSaved = DateTime.Now;
        currentSaveData.saveTime = DateTime.Now;
        currentSaveData.collectedFragmentIDs = GetCollectedFragmentIDs();
        currentSaveData.currentLevel = GetSelectedLevel();

        SaveLoadManager.SaveGame(currentSaveData);
        LoadSavedData();
    }

    void LoadGame()
    {
        currentSaveData = SaveLoadManager.LoadGame();
        if (currentSaveData != null)
        {
            ApplyLoadedData(currentSaveData);
        }
    }

    string[] GetCollectedFragmentIDs()
    {
        return new string[] { "1", "2", "3", "4", "5"};
    }

    int GetSelectedLevel()
    {
        return 1;
    }

    void ApplyLoadedData(SaveData data)
    {
        playerNameText.text = "Player Name: " + data.playerName;
        dateSavedText.text = "Date Saved: " + data.dateSaved.ToString();
        saveTimeText.text = "Time Saved: " + data.saveTime.ToString();
        currentLevelText.text = "Current Level: " + data.currentLevel.ToString();

        foreach (string fragmentID in data.collectedFragmentIDs)
        {
            int id = int.Parse(fragmentID);
            if (id >= 1 && id <= fragmentPrefabs.Length)
            {
                fragmentPrefabs[id - 1].SetActive(true);
            }
        }
    }

    void LoadSavedData()
    {
        currentSaveData = SaveLoadManager.LoadGame();
        if (currentSaveData != null)
        {
            ApplyLoadedData(currentSaveData);
        }
    }
}