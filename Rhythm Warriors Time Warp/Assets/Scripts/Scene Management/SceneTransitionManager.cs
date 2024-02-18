using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//https://youtu.be/YMj2qPq9CP8?si=n6UHdqirqBW4lNVj

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public FadeScreen fadeScreen;

    public GameObject loadingIndicator;
    private ProgressBar progressBar;

    private int currentSceneIndex;

    private void Start()
    {
        progressBar = loadingIndicator.GetComponent<ProgressBar>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(LoadNextSceneRoutine(sceneIndex));
    }

    IEnumerator LoadNextSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(fadeScreen.fadeDuration + 2f);

        loadingIndicator.SetActive(true);
        StartCoroutine(GetSceneLoadProgress(sceneIndex));
    }

    IEnumerator GetSceneLoadProgress(int sceneIndex)
    {
        progressBar.current = 0;
        progressBar.min = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float sceneProgress = operation.progress * 100f; // Get percentage

            progressBar.max = 100f;
            progressBar.current = sceneProgress;

            yield return null;
        }

        loadingIndicator.SetActive(false);
    }

    //

    public void RestartGame()
    {
        LoadNextScene(currentSceneIndex);
    }

    public void ReturnToMainMenu()
    {
        LoadNextScene((int)SceneIndexes.MAIN_MENU);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
