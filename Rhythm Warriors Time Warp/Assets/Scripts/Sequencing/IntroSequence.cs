using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public AudioSource warpSound;

    public Portal portal;
    static public bool destinationReached = false;

    private bool execute1Once = false;
    private bool execute2Once = false;

    private int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        index = DialogueManager.instance.index;
        GameManager.instance.player.isSpeaking = true;

        if (!execute1Once && index == ExecuteIndex(0))
        {
            StartCoroutine(PortalAppears());
            execute1Once = true;
        }

        if (!execute2Once && index == ExecuteIndex(1))
        {
            StartCoroutine(PlayerInvestigates());
            execute2Once = true;
        }
    }

    private int ExecuteIndex(int i)
    {
        return DialogueManager.instance.pauseIndexes[i];
    }

    IEnumerator PortalAppears()
    {
        DialogueManager.instance.PauseDialogue();

        portal.gameObject.SetActive(true);
        warpSound.Play();
        Debug.Log("Portal Appeared");

        yield return new WaitForSeconds(2);

        DialogueManager.instance.DisplayDialogueBox();
    }

    IEnumerator PlayerInvestigates()
    {
        DialogueManager.instance.PauseDialogue();

        AudioManager.instance.bgMusic.volume += 0.1f;
        //AudioManager.instance.bgMusic.priority += 20;

        yield return new WaitUntil(() => destinationReached);

        DialogueManager.instance.DisplayDialogueBox();
    }
}
