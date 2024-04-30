using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code referenced from
// How to Make a VR Game in Unity 2022 - PART 7 - User Interface
// Valem Tutorials

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        instance = this;
    }


    // Position rotation of player's head
    public Transform playerCamera;

    // Pause menu
    public Menu pauseMenu;

    // Game Over Panel
    public Menu gameOverMenu;

    // NPC and Player 
    private NPC n;
    private Player p;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        OrientMenu(pauseMenu);
    }

    public void ShowMenu(Menu m)
    {
        // Position of menu will be spawned at the position of where the player is looking
        // Spawn distance is distance menu will spawn from player's head
        m.UI.transform.position = playerCamera.position + new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * m.spawnDistance;
    }

    public void OrientMenu(Menu m)
    {
        // Menu will always face the position of the player's head
        m.UI.transform.LookAt(new Vector3(playerCamera.position.x, m.UI.transform.position.y, playerCamera.transform.position.z));

        // Menu is at the right orientation
        m.UI.transform.forward *= -1;
    }

    public void ShowDialogue(Menu m)
    {
        n = DialogueManager.instance.npc;
        p = GameManager.instance.player;

        // Position of "menu" will be spawned a position depending on which character is speaking
        // Concerning dialogue
        if (n.isSpeaking && !p.isSpeaking)
        {
            // Position of menu will be spawned in front of the npc speaking and is faced towards the player
            m.UI.transform.position = n.transform.position /*+ new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * -(m.spawnDistance + 0.5f)*/;
        }
        else
        {
            if (!n.isSpeaking && p.isSpeaking)
            {
                ShowMenu(m);
            }
        }
    }

    public void TriggerGameOverPanel()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        gameOverMenu.UI.SetActive(true);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        ShowMenu(gameOverMenu);
    }

    public void TriggerPauseMenu()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        pauseMenu.UI.SetActive(!pauseMenu.UI.activeSelf);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        ShowMenu(pauseMenu);
    }

    public void ShowInventory(Menu m)
    {
        m.UI.transform.position = new Vector3(pauseMenu.transform.position.x - 1f, pauseMenu.transform.position.y, pauseMenu.transform.position.z);
        OrientMenu(m);
    }
}