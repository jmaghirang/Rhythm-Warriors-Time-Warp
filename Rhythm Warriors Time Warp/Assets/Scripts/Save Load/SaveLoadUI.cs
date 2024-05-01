using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveLoadUI : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public GameObject[] fragmentPrefabs; // array for fragments
    public GameObject[] levelObjects; // array of level objects to activate based on current level
    public Text playerNameText;
    public Text dateSavedText;
    public Text saveTimeText;
    public Text currentLevelText;

    private SaveData currentSaveData;

    void Start()
    {
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
        LoadSavedData(); // load saved data on UI start
    }

    void SaveGame()
    {
        currentSaveData = new SaveData();
        currentSaveData.playerName = "Player"; // 
        currentSaveData.dateSaved = DateTime.Now;
        currentSaveData.saveTime = DateTime.Now;
        currentSaveData.collectedFragmentIDs = GetCollectedFragmentIDs();
        currentSaveData.currentLevel = GameManager.instance.GetCurrentLevel();

        SaveLoadManager.SaveGame(currentSaveData);
        LoadSavedData(); // reload saved data after saving
    }

    void LoadGame()
    {
        currentSaveData = SaveLoadManager.LoadGame();
        if (currentSaveData != null)
        {
            // apply loaded data to game
            ApplyLoadedData(currentSaveData);
        }
    }

    string[] GetCollectedFragmentIDs()
    {
        return new string[] { "1", "2", "3", "4", "5"};
    }

    void ApplyLoadedData(SaveData data)
    {
        // implement logic to apply loaded data to game
        playerNameText.text = "Player Name: " + data.playerName;
        dateSavedText.text = "Date Saved: " + data.dateSaved.ToString();
        saveTimeText.text = "Time Saved: " + data.saveTime.ToString();
        currentLevelText.text = "Current Level: " + data.currentLevel.ToString();

        // activate level objects based on current level
        for (int i = 0; i < levelObjects.Length; i++)
        {
            levelObjects[i].SetActive(i + 1 == data.currentLevel);
        }

        // activate collected fragment prefabs based on loaded data
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