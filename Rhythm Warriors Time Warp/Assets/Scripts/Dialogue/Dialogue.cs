using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;
    private bool isTyping = false;

    private bool isDialoguePaused = false;

    // Input on controller to continue dialogue
    // Set to primary button [X] on left controller
    // With XR Device Simulator, it is Shift + B
    public InputActionProperty continueButton;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (continueButton.action.WasPressedThisFrame() /*Input.GetKeyDown(KeyCode.R)*/)
        {
            if (isTyping)
            {
                // If the typing coroutine is currently running, complete it immediately.
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    // all code below added 12/5, doesn't work fully
// pause dialogue to start first stage of tutorial
    public void PauseDialogue()
    {
        isDialoguePaused = true;
    }

    // resume dialogue once the first stage of tutorial ends
    public void ResumeDialogue()
    {
        isDialoguePaused = false;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    // the tutorial of showing the player to use weapon
    public void StartTutorial()
    {
        PauseDialogue();
        // Wtart the tutorial here
        StartCoroutine(WeaponTutorial()); // coroutine for weapon tutorial
    }

    // end tutorial since player fulfills requirements
    public void EndTutorial()
    {
        // end the tutorial here
        ResumeDialogue();
    }

    // coroutine for the weapon tutorial
    IEnumerator WeaponTutorial()
    {
        // show tutorial steps to the player
        Debug.Log("Weapon tutorial started.");
        yield return new WaitForSeconds(5); // placeholder for tutorial duration

        // check if the tutorial ends based on some conditions (player completes action)
        if (TutorialCompleted())
        {
            EndTutorial(); // end the tutorial and resume dialogue
        }
    }

    // example method to check if the tutorial is completed
    bool TutorialCompleted()
    {
        // check if the player fulfills the requirements for tutorial completion
        // return true if tutorial is completed, false otherwise
        return true; // placeholder
    }
}