using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Some code referenced from
// https://youtu.be/PswC-HlKZqA?si=o7Q38JtiN-xi6kkS

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject dialogueBox;

    public TextMeshProUGUI charName; // Character name to display
    public TextMeshProUGUI textComponent; // Message to display

    public float textSpeed; // Speed that message is showing up at
    private bool isTyping = false;

    Message[] lines; // List of messages in scene (edited in inspector)
    Character[] characters;  // List of characters of messages in scene (edited in inspector)

    public int index = 0; // Active message

    // Input on controller to continue dialogue
    // Set to primary button [X] on left controller
    // With XR Device Simulator, it is Shift + B
    public InputActionProperty continueButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the button to continue dialogue is pressed while the dialogue box is active in the scene...
        if (continueButton.action.WasPressedThisFrame() && dialogueBox.activeSelf == true)
        {
            if (isTyping)
            {
                // If the typing coroutine is currently running, complete it immediately.
                StopAllCoroutines();
                textComponent.text = lines[index].message;
                isTyping = false;
            }
            else
            {
                // Move onto next message/line
                NextLine();
            }
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
            // Close the dialogue box
            dialogueBox.SetActive(false);

            // This line is specifically for tutorial scene for demo purposes
            SceneTransitionManager.instance.LoadNextScene((int)SceneIndexes.WILD_WEST);
        }
    }

    void DisplayMessage()
    {
        // Clear dialogue box if there is any content initally
        textComponent.text = string.Empty;
        charName.text = string.Empty;

        // Get the character name from the current messsage
        Character characterToDisplay = characters[lines[index].charID];
        charName.text = characterToDisplay.name;

        // Start typing the message
        StartCoroutine(TypeLine());
    }

    public void ShowDialogue(Message[] msgs, Character[] chars)
    {
        // Initialize values
        lines = msgs;
        characters = chars;

        dialogueBox.SetActive(true);

        DisplayMessage();
    }
}