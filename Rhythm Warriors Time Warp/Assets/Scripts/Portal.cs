using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int nextSceneIndex;

    public void GoToScene(int sceneIndex)
    {
        SceneTransitionManager.instance.LoadNextScene(sceneIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoToScene(nextSceneIndex);
        }
    }
}
