using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Active message
    private int index = 0;

    // Index paused at
    private int pIndex;

    public bool isDialoguePaused = false;

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
        if (continueButton.action.WasPressedThisFrame() /*Input.GetKeyDown(KeyCode.R)*/ && dialogueBox.activeSelf == true)
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

    /*public void PauseDialogue()
    {
        isDialoguePaused = true;
        pIndex = index;

        dialogueBox.SetActive(!isDialoguePaused);

        Debug.Log("Paused index: " + pIndex);
    }

    public void ResumeDialogue()
    {
        isDialoguePaused = false;
        index = pIndex;

        dialogueBox.SetActive(!isDialoguePaused);

        
    }*/
}