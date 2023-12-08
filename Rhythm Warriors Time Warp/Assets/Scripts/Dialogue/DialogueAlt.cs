using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code referenced from
// https://youtu.be/PswC-HlKZqA?si=o7Q38JtiN-xi6kkS

public class DialogueAlt : MonoBehaviour
{
    public Message[] messages;
    public Character[] characters;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
       DialogueManager.instance.ShowDialogue(messages, characters);
    }
}

[System.Serializable]
public class Message
{
    public int charID;
    public string message;
}

[System.Serializable]
public class Character
{
    public string name;
}
