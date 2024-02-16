using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://youtu.be/iXWFTgFNRdM?si=bpeGmg3uO_hTvEJf

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public FadeScreen fadeScreen;

    List<AsyncOperation> scenesLoading = new();
    public GameObject loadingScreen;
    public ProgressBar progressBar;

    public void GoToScene(SceneIndexes scene)
    {
        StartCoroutine(GoToSceneRoutine((int)scene));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNewGame()
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.INTRO));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void LoadNextScene(SceneIndexes scene)
    {
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.LoadSceneAsync((int)scene));

        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f; //get percentage

                progressBar.max = 100f;
                progressBar.current = totalSceneProgress;

                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
    }

    public void ReloadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
