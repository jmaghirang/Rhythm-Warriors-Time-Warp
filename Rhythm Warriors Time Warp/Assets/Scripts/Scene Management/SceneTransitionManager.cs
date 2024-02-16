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

    private void Start()
    {
        progressBar = loadingIndicator.GetComponent<ProgressBar>();
    }

    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(LoadNextSceneRoutine(sceneIndex));
    }

    IEnumerator LoadNextSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        loadingIndicator.SetActive(true);
        StartCoroutine(GetSceneLoadProgress(sceneIndex));
    }

    IEnumerator GetSceneLoadProgress(int sceneIndex)
    {
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

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
