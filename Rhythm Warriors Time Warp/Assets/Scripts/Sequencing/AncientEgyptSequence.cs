using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AncientEgyptSequence : MonoBehaviour
{
    private bool songStarted = false;

    public Fragment fragment;

    [SerializeField]
    private bool trialComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        // fragment.ID = Artifact.instance.fragments[2].ID;
        InventoryManager.instance.playingLevel = true;

        GameManager.instance.player.isSpeaking = true;
        DialogueManager.instance.npc.isSpeaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.endOfDialogue)
        {
            SongManager.instance.StartSong();
            songStarted = true;
        }

        if (!SongManager.instance.audioSource.isPlaying && !GameManager.instance.isPaused && songStarted)
        {
            trialComplete = true;
        }

        if (trialComplete)
        {
            StartCoroutine(CollectFragment());
        }

        if (fragment != null && fragment.isCollected)
        {
            InventoryManager.instance.CollectArtifact(fragment.ID.ToString()); // Collect the artifact
            // InventoryUIManager.instance.OnArtifactCollected(fragment.ID.ToString()); // Update UI
            // Artifact.instance.fragments[1].isCollected = fragment.isCollected;
            SceneTransitionManager.instance.LoadNextScene(12);
        }
    }

    IEnumerator CollectFragment()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        fragment.gameObject.SetActive(true);

        yield return new WaitUntil(() => fragment.isCollected);
    }
}