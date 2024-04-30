using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Jamie Lee

public class WildWestSequence : MonoBehaviour
{
    public Fragment fragment;

    [SerializeField]
    private bool trialComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        fragment.ID = Artifact.instance.fragments[1].ID;

        SongManager.instance.StartSong();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SongManager.instance.audioSource.isPlaying && !GameManager.instance.isPaused)
        {
            DialogueManager.instance.dialogueBox.UI.SetActive(true);
            trialComplete = true;
        }

        if (trialComplete)
        {
            StartCoroutine(CollectFragment());
        }

        if (fragment.isCollected)
        {
            Artifact.instance.fragments[1].isCollected = fragment.isCollected;
            SceneTransitionManager.instance.LoadNextScene(6);
        }
    }

    IEnumerator CollectFragment()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        fragment.gameObject.SetActive(true);

        yield return new WaitUntil(() => fragment.isCollected);
    }
}
