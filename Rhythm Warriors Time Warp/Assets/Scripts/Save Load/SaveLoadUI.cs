using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class SaveLoadUI : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI timeSavedText;
    public TextMeshProUGUI dateSavedText;

    public GameObject[] fragmentPrefabs;

    private int currentSaveSlot = 1; // default to the first save slot

    private void Start()
    {
        // initialize UI with saved data
        UpdateUITextFromPrefs();
    }

    private void UpdateUITextFromPrefs()
    {
        playerNameText.text = PlayerPrefs.GetString($"PlayerName{currentSaveSlot}", "Player");
        timeSavedText.text = PlayerPrefs.GetString($"TimeSaved{currentSaveSlot}", "00:00");
        dateSavedText.text = PlayerPrefs.GetString($"DateSaved{currentSaveSlot}", "MM/dd/yyyy");
    }

    public void SaveSlot1Clicked()
    {
        SaveDataToPrefs(1);
    }

    public void SaveSlot2Clicked()
    {
        SaveDataToPrefs(2);
    }

    public void SaveSlot3Clicked()
    {
        SaveDataToPrefs(3);
    }

    public void LoadSlot1Clicked()
    {
        LoadDataFromPrefs(1);
    }

    public void LoadSlot2Clicked()
    {
        LoadDataFromPrefs(2);
    }

    public void LoadSlot3Clicked()
    {
        LoadDataFromPrefs(3);
    }

    private void SaveDataToPrefs(int slotNumber)
    {
        PlayerPrefs.SetString($"PlayerName{slotNumber}", playerNameText.text);
        PlayerPrefs.SetString($"TimeSaved{slotNumber}", DateTime.Now.ToShortTimeString());
        PlayerPrefs.SetString($"DateSaved{slotNumber}", DateTime.Today.ToShortDateString());

        // dummy collected fragment IDs for testing
        string[] collectedFragmentIDs = new string[] { "Fragment1", "Fragment2", "Fragment3", "Fragment4", "Fragment5" };
        SaveCollectedFragmentsToPrefs(slotNumber, collectedFragmentIDs);

        PlayerPrefs.Save();

        UpdateUITextFromPrefs();
    }

    private void SaveCollectedFragmentsToPrefs(int slotNumber, string[] collectedFragmentIDs)
    {
        string json = JsonUtility.ToJson(new SerializableStringArray(collectedFragmentIDs));
        PlayerPrefs.SetString($"CollectedFragments{slotNumber}", json);
    }

    private void LoadDataFromPrefs(int slotNumber)
    {
        currentSaveSlot = slotNumber;
        UpdateUITextFromPrefs();

        string json = PlayerPrefs.GetString($"CollectedFragments{slotNumber}", string.Empty);
        SerializableStringArray serializedArray = JsonUtility.FromJson<SerializableStringArray>(json);
        if (serializedArray != null)
        {
            // load 3D model fragments based on collected fragment IDs
            Load3DModelFragments(serializedArray.array);
        }
    }

    private void Load3DModelFragments(string[] fragmentIDs)
    {
        foreach (GameObject fragmentPrefab in fragmentPrefabs)
        {
            Instantiate(fragmentPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}

[Serializable]
public class SerializableStringArray
{
    public string[] array;

    public SerializableStringArray(string[] array)
    {
        this.array = array;
    }
}