using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageIntroSequence : MonoBehaviour
{
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.instance.DisplayDialogueBox();
    }

    // Update is called once per frame
    void Update()
    {
        index = DialogueManager.instance.index;

        SetSpeaker(false, true);

        if (DialogueManager.instance.endOfDialogue)
        {
            SceneTransitionManager.instance.LoadNextScene((int)SceneIndexes.TUTORIAL);
            DialogueManager.instance.endOfDialogue = false;
        }
    }

    private int ExecuteIndex(int i)
    {
        return DialogueManager.instance.pauseIndexes[i];
    }

    public void SetSpeaker(bool player, bool npc)
    {
        for (int i = 0; i < DialogueManager.instance.pauseIndexes.Count; i++)
        {
            if (index == ExecuteIndex(i))
            {
                if (i % 2 == 0)
                {
                    GameManager.instance.player.isSpeaking = player;
                    DialogueManager.instance.npc.isSpeaking = npc;
                }
                else
                {
                    GameManager.instance.player.isSpeaking = !player;
                    DialogueManager.instance.npc.isSpeaking = !npc;
                }  
            }
             
        }
    }
}
