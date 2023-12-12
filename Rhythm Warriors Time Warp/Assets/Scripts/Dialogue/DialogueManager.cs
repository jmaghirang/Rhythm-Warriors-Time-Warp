using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Code referenced from
// https://youtu.be/PswC-HlKZqA?si=o7Q38JtiN-xi6kkS

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject dialogueBox;

    public TextMeshProUGUI charName;
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private bool isTyping = false;

    Message[] lines;
    Character[] characters;

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
                NextLine();
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        foreach (char c in lines[index].message.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            DisplayMessage();
        }
        else
        {
            dialogueBox.SetActive(false);

            // Specifically for tutorial scene for demo purposes
            SceneManager.LoadScene("Wild West");
        }
    }

    void DisplayMessage()
    {
        textComponent.text = string.Empty;
        charName.text = string.Empty;

        Character characterToDisplay = characters[lines[index].charID];
        charName.text = characterToDisplay.name;

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