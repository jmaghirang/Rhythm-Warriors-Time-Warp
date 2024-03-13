using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
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
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedLevels[0])
        {
            SetLevel("westSelected");
            SetTitles(levelTitles[0].gameObject);
        }

        else if (selectedLevels[1])
        {
            SetLevel("japanSelected");
            SetTitles(levelTitles[1].gameObject);
        }

        else if (selectedLevels[2])
        {
            SetLevel("greeceSelected");
            SetTitles(levelTitles[2].gameObject);
        }

        else if (selectedLevels[3])
        {
            SetLevel("egyptSelected");
            SetTitles(levelTitles[3].gameObject);
        }

        else if (selectedLevels[4])
        {
            SetLevel("futureSelected");
            SetTitles(levelTitles[4].gameObject);
        }
    }

    public void SetTitles(GameObject activeTitle)
    {
        foreach (Canvas c in levelTitles)
        {
            c.gameObject.SetActive(false);
        }

        activeTitle.SetActive(true);
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

    public void NextLevel(List<bool> levels)
    {
        if (i >= 4)
        {
            i = 0;
        }

        levels[i] = false;
        levels[i + 1] = true;

        i++;
    }

    public void PrevLevel(List<bool> levels)
    {
        if (i <= 0)
        {
            i = 4;
        }

        levels[i] = false;
        levels[i - 1] = true;

        i--;
    }
}
