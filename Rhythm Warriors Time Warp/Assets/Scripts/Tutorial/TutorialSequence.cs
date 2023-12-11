using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialSequence : MonoBehaviour
{
    public int rIndex = 0;

    // Keeps track of what is called once - very not efficient will change later
    private bool execute1Once = false;
    private bool execute2Once = false;
    private bool execute3Once = false;
    private bool execute4Once = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rIndex = DialogueManager.instance.index;

        if (!execute1Once && rIndex == 8)
        {
            StartCoroutine(SwingWeapon());
            execute1Once = true;
        }

        if (!execute2Once && rIndex == 10)
        {
            StartCoroutine(DodgeObstacles());
            execute2Once = true;
        }

        if (!execute3Once && rIndex == 12)
        {
            StartCoroutine(HitEnemies());
            execute3Once = true;
        }

        if (!execute4Once && rIndex == 14)
        {
            StartCoroutine(HealthDrain());
            execute4Once = true;
        }
    }

    public IEnumerator SwingWeapon()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Player does stuff

        yield return new WaitForSeconds(5f);

        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator DodgeObstacles()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Player does stuff

        yield return new WaitForSeconds(5f);

        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HitEnemies()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        SongManager.instance.StartSong();

        yield return new WaitForSeconds(10f);

        SongManager.instance.StopSong();
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HealthDrain()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // UI display

        yield return new WaitForSeconds(5f);

        DialogueManager.instance.dialogueBox.SetActive(true);
    }
}
