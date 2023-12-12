using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class TutorialSequence : MonoBehaviour
{
    public int rIndex = 0;

    public GameObject weapon;
    public GameObject healthBar;
    public GameObject obstacle;

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
        weapon.SetActive(true);

        yield return new WaitForSeconds(5f);

        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator DodgeObstacles()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        obstacle.SetActive(true);

        yield return new WaitForSeconds(5f);

        obstacle.SetActive(false);
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HitEnemies()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        StartCoroutine(SongManager.instance.StartSong());

        yield return new WaitUntil(() => ScoreManager.instance.currentScore == 6);

        SongManager.instance.StopSong();
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HealthDrain()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        healthBar.SetActive(true);
        GameManager.instance.player.TakeDamage(10);

        yield return new WaitForSeconds(5f);

        healthBar.SetActive(false);
        DialogueManager.instance.dialogueBox.SetActive(true);
    }
}
