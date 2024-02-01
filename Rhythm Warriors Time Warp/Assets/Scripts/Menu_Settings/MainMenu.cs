using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int sceneIndex;

    public GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new();
    public ProgressBar progressBar;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


    public void PlayGame()
    {
        if (sceneIndex == 5) {
            sceneIndex = 0;
        }
        SceneManager.LoadScene (sceneIndex + 1);
    }

    public void LoadNewGame()
    {
        //SceneManager.LoadScene("Tutorial");
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.TUTORIAL));
        //scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.WILD_WEST));

        StartCoroutine(GetSceneLoadProgress());
    }

    float totalSceneProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
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
        Debug.Log ("QUIT!");
        Application.Quit();
    }
}