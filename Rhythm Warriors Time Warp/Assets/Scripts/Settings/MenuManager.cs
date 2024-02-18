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

    // Distance menu will spawn from player's head
    public float spawnDistance = 0.5f;

    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrientMenu(pauseMenu);
    }

    public void ShowMenu(GameObject m)
    {
        // Position of menu will be spawned at the position of where the player is looking
        m.transform.position = playerCamera.position + new Vector3(playerCamera.forward.x, 0, playerCamera.forward.z).normalized * spawnDistance;
    }

    public void OrientMenu(GameObject m)
    {
        // Menu will always face the position of the player's head
        m.transform.LookAt(new Vector3(playerCamera.position.x, m.transform.position.y, playerCamera.transform.position.z));

        // Menu is at the right orientation
        m.transform.forward *= -1;
    }

    public void TriggerPauseMenu()
    {
        // Menu will be shown or put away depending on it's currently showing up or not
        pauseMenu.SetActive(!pauseMenu.activeSelf);

        // Position of pauseMenu will be spawned at the position of where the player is looking
        ShowMenu(pauseMenu);
    }
}
