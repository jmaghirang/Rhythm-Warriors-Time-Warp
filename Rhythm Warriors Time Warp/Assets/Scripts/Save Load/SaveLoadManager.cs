using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveGame(SaveData data)
    {
        PlayerPrefs.SetString("PlayerName", data.playerName);
        PlayerPrefs.SetString("DateSaved", data.dateSaved.ToString());
        PlayerPrefs.SetString("SaveTime", data.saveTime.ToString());
        PlayerPrefs.SetString("CollectedFragmentIDs", string.Join(",", data.collectedFragmentIDs));
        PlayerPrefs.SetInt("CurrentLevel", data.currentLevel);
        PlayerPrefs.Save();
    }

    public static SaveData LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            SaveData data = new SaveData();
            data.playerName = PlayerPrefs.GetString("PlayerName");
            data.dateSaved = System.DateTime.Parse(PlayerPrefs.GetString("DateSaved"));
            data.saveTime = System.DateTime.Parse(PlayerPrefs.GetString("SaveTime"));
            data.collectedFragmentIDs = PlayerPrefs.GetString("CollectedFragmentIDs").Split(',');
            data.currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            return data;
        }
        else
        {
            Debug.LogError("Save data not found.");
            return null;
        }
    }
}