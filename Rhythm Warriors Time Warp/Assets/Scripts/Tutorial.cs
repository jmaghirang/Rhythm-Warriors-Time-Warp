using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    public GameObject[] dialogueBoxes; // array to hold all dialogue box panels
    public GameObject enemyPrefab; // reference to the enemy prefab
    public Transform spawnPoint; // point where the enemy will spawn
    private int currentDialogueIndex = 0;
    private bool enemySpawned = false;

    void Start()
    {
        ShowDialogueBox(currentDialogueIndex); // show the initial dialogue box
    }

    void ShowDialogueBox(int index)
    {
        // hide all dialogue boxes
        foreach (GameObject box in dialogueBoxes)
        {
            box.SetActive(false);
        }

        // show the dialogue box at the specified index
        dialogueBoxes[index].SetActive(true);

        // check if this is the dialogue for enemy spawning
        if (index == 0)
        {
            // spawn the enemy when the corresponding dialogue appears
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (!enemySpawned)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemySpawned = true;
        }
    }

    public void PlayerSliceEnemy()
    {
        // check if the player has sliced the enemy
        // use collision detection

        // sliced for now lol
        if (IsEnemySliced())
        {
            currentDialogueIndex++; // move to the next dialogue
            enemySpawned = false; // reset enemy spawn flag
            ShowDialogueBox(currentDialogueIndex);
        }
        else
        {
            // player hasn't sliced the enemy yet, can show a hint or repeat the same dialogue
            Debug.Log("Slice the enemy to proceed!");
        }
    }

    bool IsEnemySliced()
    {
        // return true if the enemy is successfully sliced, otherwise return false.
        return false;
    }
}