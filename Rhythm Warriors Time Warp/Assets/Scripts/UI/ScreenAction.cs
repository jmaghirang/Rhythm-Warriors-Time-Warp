using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenAction : MonoBehaviour
{
    public Button screen;
    public AudioSource openScreenSound;
    public AudioSource mouseClickSound;

    public Animator anim;

    public GameObject panels;

    // Start is called before the first frame update
    void Start()
    {
        screen.onClick.AddListener(() => StartCoroutine(OnScreenPressed()));
        screen.onClick.AddListener(() => mouseClickSound.Play());
    }

    IEnumerator OnScreenPressed()
    {
        anim.SetTrigger("screenPressed");
        openScreenSound.Play();

        yield return new WaitForSeconds(1f);

        panels.SetActive(true);
        screen.gameObject.SetActive(false);
    }
}
