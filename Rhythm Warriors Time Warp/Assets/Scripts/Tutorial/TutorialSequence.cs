using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class TutorialSequence : MonoBehaviour
{
    public int rIndex = 0;

    public GameObject weapon; // Weapon player will be shown holding
    public GameObject healthBar; // Health bar to show to player
    public GameObject obstacle; // Obstacle to display to player

    // Keeps track of what is called once - definitely not very efficient will change later
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
        // Keeping track of the current dialogue showing
        rIndex = DialogueManager.instance.index;

        // bool variables to make sure each corouting runs exactly once
        if (!execute1Once && rIndex == 8)
        {
            // If dialgoue to instruct user to swing weapon is reached, show the weapon to the player and give them time to swing the weapon
            StartCoroutine(SwingWeapon());
            execute1Once = true;
        }

        if (!execute2Once && rIndex == 10)
        {
            // If dialogue to show player what obstacles look like is reached, show obstacle to player
            StartCoroutine(DodgeObstacles());
            execute2Once = true;
        }

        if (!execute3Once && rIndex == 12)
        {
            // If dialogue to instruct player to hit enemies is reached, start song to simluate gameplay
            StartCoroutine(HitEnemies());
            execute3Once = true;
        }

        if (!execute4Once && rIndex == 14)
        {
            // If dialogue to show player how health can deplete is reached, show player the health bar
            StartCoroutine(HealthDrain());
            execute4Once = true;
        }
    }

    public IEnumerator SwingWeapon()
    {
        // Make the dialogue box go away
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Show the player the weapon in their hand
        weapon.SetActive(true);

        // Wait for player to experiment with the weapon
        yield return new WaitForSeconds(5f);

        // Make the dialgoue box reappear
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator DodgeObstacles()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Show the player an obstacle in front of them
        obstacle.SetActive(true);

        // Wait for player to get a good look at obstacle
        yield return new WaitForSeconds(5f);

        // Make the obstacle go away
        obstacle.SetActive(false);
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HitEnemies()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Simulate rhythm gameplay
        StartCoroutine(SongManager.instance.StartSong());

        // Wait until the player has hit a certain amount of enemies
        yield return new WaitUntil(() => ScoreManager.instance.currentScore == 6);

        // Stop the song to stop gameplay
        SongManager.instance.StopSong();
        DialogueManager.instance.dialogueBox.SetActive(true);
    }

    public IEnumerator HealthDrain()
    {
        DialogueManager.instance.dialogueBox.SetActive(false);
        // Show player what a health bar looks like
        healthBar.SetActive(true);
        // Deplete the health bar a bit to demonstrate to player
        GameManager.instance.player.TakeDamage(10);

        // Wait for player to get a good look
        yield return new WaitForSeconds(5f);

        // Make health bar go away
        healthBar.SetActive(false);
        DialogueManager.instance.dialogueBox.SetActive(true);
    }
}
