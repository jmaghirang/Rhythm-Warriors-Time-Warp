using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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


    // ------------------------------------------------------------
    // Defining attributes
    // ------------------------------------------------------------


    // Position rotation of player's head
    public Transform head;

    // Distance menu will spawn from player's head
    public float spawnDistance = 0.5f;

    // Canvas/menu to show
    public GameObject menu;

    // Input on controller to trigger menu
    public InputActionProperty menuButton;


    // ------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the button specified was pressed
        if (menuButton.action.WasPressedThisFrame())
        {
            // Menu will be shown or put away depending on it's currently showing up or not
            menu.SetActive(!menu.activeSelf);

            // Position of menu will be at the position of the player's head but will face in the direction of where the player is looking
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        // Menu will always face the position of the player's head
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.transform.position.z));

        // Menu is at the right orientation
        menu.transform.forward *= -1;
    }
}
