using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using static UnityEngine.Rendering.DebugUI;

public class TutorialSequence : MonoBehaviour
{
    private int index;

    public GameObject inventoryBtn;
    public GameObject weapon; // Weapon player will be shown holding
    public GameObject healthBar; // Health bar to show to player
    public GameObject timeBar; // Song progress bar to show to player
    public GameObject nameInput; // Field for player to input their name
    //public GameObject confirmationButton;

    private bool nameSaved = false;

    // Keeps track of what is called once
    private bool execute1Once = false;
    private bool execute2Once = false;
    private bool execute3Once = false;
    private bool execute4Once = false;
    private bool execute5Once = false;
    private bool execute6Once = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Keeping track of the current dialogue showing
        index = DialogueManager.instance.index;
        GameManager.instance.player.isSpeaking = false;
        DialogueManager.instance.npc.isSpeaking = true;

        // Bool variables to make sure each coroutine runs exactly once
        if (!execute1Once && index == ExecuteIndex(0))
        {
            StartCoroutine(GiveItemsToPlayer());
            execute1Once = true;
        }

        if (!execute2Once && index == ExecuteIndex(1))
        {
            // If dialgoue to instruct user to swing weapon is reached, show the weapon to the player and give them time to swing the weapon
            StartCoroutine(SwingWeapon());
            execute2Once = true;
        }

        if (!execute3Once && index == ExecuteIndex(2))
        {
            // If dialogue to instruct player to hit enemies is reached, start song to simluate gameplay
            StartCoroutine(HitEnemies());
            execute3Once = true;
        }

        if (!execute4Once && index == ExecuteIndex(3))
        {
            StartCoroutine(GiveTracker());
            execute4Once = true;
        }

        if (!execute5Once && index == ExecuteIndex(4))
        {
            StartCoroutine(ShowProgressBar());
            execute5Once = true;
        }

        if (!execute6Once && index == ExecuteIndex(5))
        {
            StartCoroutine(GetPlayerName());
            execute6Once = true;
        }

        if (DialogueManager.instance.endOfDialogue)
        {
            SceneTransitionManager.instance.LoadNextScene((int)SceneIndexes.VILLAGE_AFTER_TUTORIAL);
            DialogueManager.instance.endOfDialogue = false;
        }
    }

    private int ExecuteIndex(int i)
    {
        return DialogueManager.instance.pauseIndexes[i];
    }

    public IEnumerator GiveItemsToPlayer()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        // Show the player the weapon in their hand
        weapon.SetActive(true);
        InventoryManager.instance.inventory.gameObject.SetActive(true);
        inventoryBtn.SetActive(true);

        // Code to show player items received
        yield return new WaitForSeconds(3f);

        InventoryManager.instance.inventory.gameObject.SetActive(false);
        inventoryBtn.SetActive(false);

        DialogueManager.instance.dialogueBox.UI.SetActive(true);

        InventoryManager.instance.hasInventory = true;
    }

    public IEnumerator SwingWeapon()
    {
        // Make the dialogue box go away
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        // Wait for player to experiment with the weapon
        yield return new WaitForSeconds(3f);

        // Make the dialgoue box reappear
        DialogueManager.instance.dialogueBox.UI.SetActive(true);
    }

    public IEnumerator HitEnemies()
    {
        GameManager.instance.playingTutorial = true;

        DialogueManager.instance.dialogueBox.UI.SetActive(false);
        healthBar.SetActive(true);

        // Simulate rhythm gameplay
        SongManager.instance.StartSong();

        // Wait until the player has hit a certain amount of enemies
        yield return new WaitUntil(() => ScoreManager.instance.GetCurrentScore() > 600 || ScoreManager.instance.GetCurrentMisses() > 10);

        // Stop the song to stop gameplay
        SongManager.instance.StopSong();

        if (ScoreManager.instance.GetCurrentMisses() > 10)
        {
            GameManager.instance.PauseGame();
            MenuManager.instance.gameOverMenu.UI.SetActive(true);

            GameManager.instance.isGameOver = true;
        }
        else
        {
            healthBar.SetActive(false);
            DialogueManager.instance.dialogueBox.UI.SetActive(true);

            GameManager.instance.playingTutorial = false;
        }
    }

    public IEnumerator GiveTracker()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        // Code to show player tracker recieved
        //
        yield return new WaitForSeconds(1f);

        DialogueManager.instance.dialogueBox.UI.SetActive(true);
    }

    public IEnumerator ShowProgressBar()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);
        timeBar.SetActive(true);

        SongManager.instance.StartSong();

        yield return new WaitForSeconds(10f);

        SongManager.instance.StopSong();

        timeBar.SetActive(false);
        DialogueManager.instance.dialogueBox.UI.SetActive(true);
    }

    public IEnumerator GetPlayerName()
    {
        DialogueManager.instance.dialogueBox.UI.SetActive(false);

        nameInput.SetActive(true);

        yield return new WaitUntil(() => nameSaved == true);

        nameInput.SetActive(false);

        DialogueManager.instance.dialogueBox.UI.SetActive(true);
    }

    public void SavePlayerName()
    {
        if (ShowKeyboard.instance.inputField.text == "")
        {
            NonNativeKeyboard.Instance.Clear();
            ShowKeyboard.instance.OpenKeyboard();
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", NonNativeKeyboard.Instance.InputField.text);

            nameSaved = true;
        }
    }
}