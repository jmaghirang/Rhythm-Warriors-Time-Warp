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

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        StartCoroutine(GetSceneLoadProgress(sceneIndex));
    }

    IEnumerator GetSceneLoadProgress(int sceneIndex)
    {
        loadingIndicator.SetActive(true);

        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        progressBar.current = 0;

        while (!operation.isDone)
        {
            yield return new WaitForEndOfFrame();

            float sceneProgress = Mathf.Clamp01(operation.progress / 0.09f);

            progressBar.max = 100f;
            progressBar.current = sceneProgress * 100f;

            if (operation.progress >= 0.9f)
            {
                progressBar.current = sceneProgress * 100f;
                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Current: " + progressBar.current);
            Debug.Log("Fill: " + progressBar.mask.fillAmount);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        LoadNextScene(currentSceneIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        LoadNextScene((int)SceneIndexes.MAIN_MENU);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
