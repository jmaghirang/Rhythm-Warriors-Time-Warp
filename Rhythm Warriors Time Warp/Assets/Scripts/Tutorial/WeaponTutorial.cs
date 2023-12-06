using UnityEngine;
using System.Collections;

public class WeaponTutorial : MonoBehaviour
{
    public Dialogue dialogue; // reference to the dialogue system
    public GameObject tutorialUI; // reference to the tutorial UI
    public GameObject dummyEnemy;
    public GameObject dummyEnemyAttacked;
    
    void Start()
    {
        // start the tutorial when required, maybe triggered from another script or event
        StartWeaponTutorial();
    }

    public void StartWeaponTutorial()
    {
        StartCoroutine(WeaponTutorialCoroutine());
    }

    IEnumerator WeaponTutorialCoroutine()
    {
        Debug.Log("Weapon tutorial started.");

        // show tutorial UI
        tutorialUI.SetActive(true);

        yield return new WaitForSeconds(2);

        dialogue.textComponent.text = "Attack the dummy enemy by pressing 'Attack' button";

        // enable the dummy enemy to be interacted with
        dummyEnemy.SetActive(true);

        while (!DummyEnemyAttacked())
        {
            yield return null;
        }

        // end of tutorial or move to the next step based on your requirements
        EndTutorial();
    }

    bool DummyEnemyAttacked()
    {
    // Assuming dummyEnemyAttacked is a boolean variable that indicates whether the dummy enemy has been attacked
        return dummyEnemyAttacked;
    }


    void EndTutorial()
    {
        // hide tutorial UI
        tutorialUI.SetActive(false);

        // disable or deactivate the dummy enemy
        dummyEnemy.SetActive(false);

        // resume dialogue
        dialogue.ResumeDialogue();
    }
}
