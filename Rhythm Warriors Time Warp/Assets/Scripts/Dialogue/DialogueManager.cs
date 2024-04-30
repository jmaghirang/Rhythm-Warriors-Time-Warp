using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

// Some code referenced from
// https://youtu.be/PswC-HlKZqA?si=o7Q38JtiN-xi6kkS

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Dialogue Box UI
    public Menu dialogueBox;

    // NPC
    public NPC npc;

    public TextMeshProUGUI charName; // Character name to display
    public TextMeshProUGUI textComponent; // Message to display
    public GameObject indicateContinue; // Image to display to user to help them know the control to continue dialogue

    public float textSpeed; // Speed that message is showing up at
    private bool isTyping = false;

    Message[] lines; // List of messages in scene (edited in inspector)
    Character[] characters;  // List of characters of messages in scene (edited in inspector)
    public int index = 0; // Index of active message

    public List<int> pauseIndexes = new(); // List of instances at when dialogue needs to be paused/adjusted in a scene

    public bool endOfDialogue = false; // Boolean signifying end of dialogue/messages

    public bool fixedBox = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the button to continue dialogue is pressed while the dialogue box is active in the scene and the game is not paused...
        if (ControlManager.instance.continueButton.action.WasPressedThisFrame() && !GameManager.instance.isPaused && dialogueBox.UI.activeSelf == true)
        {
            if (isTyping)
            {
                // If the typing coroutine is currently running, complete it immediately.
                StopAllCoroutines();
                textComponent.text = lines[index].message;
                isTyping = false;

                indicateContinue.SetActive(true);
            }
            else
            {
                // Move onto next message/line and position dialogue box in front of player
                NextLine();
            }
        }

        // Always make the dialogue box rotate to face the player
        if (!fixedBox)
        {
            MenuManager.instance.OrientMenu(dialogueBox);
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        // For each character in the message, type each character out waiting a certain amount of time before the next character is typed out
        foreach (char c in lines[index].message.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        indicateContinue.SetActive(true);
    }

    void NextLine()
    {
        // Increment the index to the next message
        index++;

        // If we aren't at the end of the list of messages...
        if (index < lines.Length)
        {
            // Display the message
            DisplayMessage();
        }
        else
        {
            // End of messages
            // Close the dialogue box
            dialogueBox.UI.SetActive(false);
            endOfDialogue = true;
        }
    }

    void DisplayMessage()
    {
        // Clear dialogue box if there is any content initially
        textComponent.text = string.Empty;
        charName.text = string.Empty;
        // Set continue indicator to false
        indicateContinue.SetActive(false);

        // Get the character name from the current messsage
        Character characterToDisplay = characters[lines[index].charID];
        charName.text = characterToDisplay.name;

        // Show the dialogue box in front of player
        DisplayDialogueBox();

        // Start typing the message
        StartCoroutine(TypeLine());
    }

    public void DisplayDialogue(Message[] msgs, Character[] chars)
    {
        // Initialize values
        lines = msgs;
        characters = chars;

        DisplayMessage();
    }

    public void PauseDialogue()
    {
        dialogueBox.UI.SetActive(false);
    }

    public void DisplayDialogueBox()
    {
        if (!fixedBox)
        {
            //MenuManager.instance.OrientMenu(dialogueBox);
            MenuManager.instance.ShowDialogue(dialogueBox);
        }
        
        dialogueBox.UI.SetActive(true);
    }
}
