using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private string playerPrefsKey = "lastPlayedSceneIndex"; // the PlayerPrefs key for storing the last played scene index

    public void Save()
    {
        // save the index of the current scene
        int lastPlayedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(playerPrefsKey, lastPlayedSceneIndex);
        PlayerPrefs.Save(); // save changes to PlayerPrefs immediately
    }

    public void Load()
    {
        // load the index of the last played scene from PlayerPrefs
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            int lastPlayedSceneIndex = PlayerPrefs.GetInt(playerPrefsKey);
            // load the last played scene
            SceneManager.LoadScene(lastPlayedSceneIndex);
        }
    }
}