using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public List<Canvas> levelTitles;

    public List<bool> selectedLevels = new();

    private bool wildWestSelected = true;
    private bool feudalJapanSelected = false;
    private bool ancientGreeceSelected = false;
    private bool ancientEgyptSelected = false;
    private bool futureSelected = false;

    public Animator levelSelectAnimator;

    public Button next;
    public Button prev;

    public Button confirm;

    public TextMeshProUGUI levelText;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Canvas c in levelTitles)
        {
            c.gameObject.SetActive(false);
        }

        selectedLevels.Add(wildWestSelected);
        selectedLevels.Add(feudalJapanSelected);
        selectedLevels.Add(ancientGreeceSelected);
        selectedLevels.Add(ancientEgyptSelected);
        selectedLevels.Add(futureSelected);

        next.onClick.AddListener(() => NextLevel(selectedLevels));
        prev.onClick.AddListener(() => PrevLevel(selectedLevels));
        confirm.onClick.AddListener(() => GoToLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedLevels[0])
        {
            SetLevel("westSelected");
            StartCoroutine(SetTitles(levelTitles[0].gameObject));

            levelText.text = "The Wild West";
        }

        else if (selectedLevels[1])
        {
            SetLevel("japanSelected");
            StartCoroutine(SetTitles(levelTitles[1].gameObject));

            levelText.text = "Feudal Japan";
        }

        else if (selectedLevels[2])
        {
            SetLevel("greeceSelected");
            StartCoroutine(SetTitles(levelTitles[2].gameObject));

            levelText.text = "Ancient Greece";
        }

        else if (selectedLevels[3])
        {
            SetLevel("egyptSelected");
            StartCoroutine(SetTitles(levelTitles[3].gameObject));

            levelText.text = "Ancient Egypt";
        }

        else if (selectedLevels[4])
        {
            SetLevel("futureSelected");
            StartCoroutine(SetTitles(levelTitles[4].gameObject));

            levelText.text = "The Future";
        }
    }

    public void ResetSelectedLevels()
    {
        for (int j = 0; j < selectedLevels.Count; j++)
        {
            selectedLevels[j] = false;
        }
    }

    IEnumerator SetTitles(GameObject activeTitle)
    {
        foreach (Canvas c in levelTitles)
        {
            if (c.gameObject != activeTitle && c.gameObject.activeInHierarchy)
            {
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    Image titleImage = c.GetComponentInChildren<Image>();
                    titleImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }

                c.gameObject.SetActive(false);
            }
        }

        activeTitle.SetActive(true);

        for (float i = 0; i <=1; i += Time.deltaTime)
        {
            Image activeTitleImage = activeTitle.GetComponentInChildren<Image>();
            activeTitleImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public void ResetTriggers()
    {
        foreach (var param in levelSelectAnimator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Trigger)
            {
                levelSelectAnimator.ResetTrigger(param.name);
            }
        }
    }

    public void SetLevel(string trigger)
    {
        ResetTriggers();
        levelSelectAnimator.SetTrigger(trigger);
    }

    IEnumerator ButtonDelay(Button b)
    {
        yield return new WaitForSeconds(2f);
        b.interactable = true;
    }

    public void NextLevel(List<bool> levels)
    {
        //StopAllCoroutines();

        ResetSelectedLevels();

        if (i >= 4)
        {
            i = 0;
            levels[i] = true;
        }
        else
        {
            levels[i + 1] = true;
            i++;
        }

        next.interactable = false;
        StartCoroutine(ButtonDelay(next));
    }

    public void PrevLevel(List<bool> levels)
    {
        //StopAllCoroutines();

        ResetSelectedLevels();

        if (i <= 0)
        {
            i = 4;
            levels[i] = true;
        }
        else
        {
            levels[i - 1] = true;
            i--;
        }

        prev.interactable = false;
        StartCoroutine(ButtonDelay(prev));
    }

    // some code from https://youtu.be/1VXeyeLthdQ?si=EVq-RXS0WxIY4Dkz
    /*IEnumerator PlayPreview(AudioSource preview)
    {  
        float timeToFade = 1.25f;
        float timeElapsed = 0;

        preview.Play();

        if (preview.clip != wildWestPreview.clip && wildWestPreview.isPlaying)
        {
            while (timeElapsed < timeToFade)
            {
                preview.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                wildWestPreview.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
            
            wildWestPreview.Stop();
        }

        else if (preview.clip != japanPreview.clip && japanPreview.isPlaying)
        {   
            while (timeElapsed < timeToFade)
            {
                preview.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                japanPreview.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            japanPreview.Stop();
        }

        else if (preview.clip != greecePreview.clip && greecePreview.isPlaying)
        {
            while (timeElapsed < timeToFade)
            {
                preview.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                greecePreview.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            greecePreview.Stop();
        }

        else if (preview.clip != egyptPreview.clip && egyptPreview.isPlaying)
        {
            while (timeElapsed < timeToFade)
            {
                preview.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                egyptPreview.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;

                yield return null;
            }
            egyptPreview.Stop();
        }

        else if (preview.clip != futurePreview.clip && futurePreview.isPlaying)
        {
            while (timeElapsed < timeToFade)
            {
                preview.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                futurePreview.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            futurePreview.Stop();
        }
    }*/

    public void GoToLevel()
    {
        if (selectedLevels[0])
        {
            SceneTransitionManager.instance.LoadNextScene((int)SceneIndexes.WILD_WEST);
        }

        else if (selectedLevels[2])
        {
            SceneTransitionManager.instance.LoadNextScene((int)SceneIndexes.FEUDAL_JAPAN);
        }
    }
}
