using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code referenced from
// https://youtu.be/PswC-HlKZqA?si=o7Q38JtiN-xi6kkS

public class Dialogue : MonoBehaviour
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
       DialogueManager.instance.DisplayDialogue(messages, characters);
    }
}

// Serializable so classes can be edited in inspector
[System.Serializable]
public class Message
{
    public int charID; //ID of character saying the message
    public string message;
}

[System.Serializable]
public class Character
{
    public string name;
}
