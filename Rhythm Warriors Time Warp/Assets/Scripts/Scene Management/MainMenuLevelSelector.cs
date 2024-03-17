using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLevelSelector : MonoBehaviour
{
    public List<Button> levelButtons;

    public List<AudioSource> previews;

    public List<bool> selectedLevels = new();

    private bool wildWestSelected = true;
    private bool feudalJapanSelected = false;
    private bool ancientGreeceSelected = false;
    private bool ancientEgyptSelected = false;
    private bool futureSelected = false;

    public Button next;
    public Button prev;

    public AudioFadeOutOnSwap outTransition;
    public AudioFadeInOnSwap inTransition;

    public TextMeshProUGUI levelNumber;
    public TextMeshProUGUI songTitle;
    public TextMeshProUGUI songDuration;

    public GameObject headphones;
    public AudioSource headphonesMusic;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button b in levelButtons)
        {
            b.gameObject.SetActive(false);
        }

        selectedLevels.Add(wildWestSelected);
        selectedLevels.Add(feudalJapanSelected);
        selectedLevels.Add(ancientGreeceSelected);
        selectedLevels.Add(ancientEgyptSelected);
        selectedLevels.Add(futureSelected);

        next.onClick.AddListener(() => NextLevel(selectedLevels));
        prev.onClick.AddListener(() => PrevLevel(selectedLevels));
    }

    void Awake()
    {
        headphonesMusic.Stop();

        inTransition.preview = previews[i];
        StartCoroutine(inTransition.StartTrack());
    }

    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < selectedLevels.Count; index++)
        {
            if (selectedLevels[i])
            {
                levelButtons[i].gameObject.SetActive(true);
                levelNumber.text = "Level " + (i + 1).ToString();
            }
            else
            {
                continue;
            }
        }

        // Level-Song Information
        
        // Wild West
        if (selectedLevels[0])
        {
            songTitle.text = "Cowboy Hat - Cold Cinema";
            songDuration.text = "2:10";
        }

        // Japan
        else if (selectedLevels[1])
        {
            songTitle.text = "Shamisen Samurai Rock - MOJI";
            songDuration.text = "2:01";
        }

        // Greece
        else if (selectedLevels[2])
        {
            songTitle.text = "";
            songDuration.text = "";
        }

        // Egypt
        else if (selectedLevels[3])
        {
            songTitle.text = "";
            songDuration.text = "";
        }

        // Future
        else if (selectedLevels[4])
        {
            songTitle.text = "";
            songDuration.text = "";
        }
    }

    public void ResetSelectedLevels()
    {
        for (int j = 0; j < selectedLevels.Count; j++)
        {
            selectedLevels[j] = false;
        }
    }

    public void NextLevel(List<bool> levels)
    {
        ResetSelectedLevels();

        if (i >= 4)
        {
            i = 0;
            outTransition.preview = previews[4];
            levelButtons[4].gameObject.SetActive(false);
            levels[i] = true;
        }
        else
        {
            outTransition.preview = previews[i];
            levelButtons[i].gameObject.SetActive(false);
            levels[i + 1] = true;
            i++;
        }

        StartCoroutine(outTransition.SwapTrack());

        inTransition.preview = previews[i];
        StartCoroutine(inTransition.StartTrack());
    }

    public void PrevLevel(List<bool> levels)
    {
        ResetSelectedLevels();

        if (i <= 0)
        {
            i = 4;
            outTransition.preview = previews[0];
            levelButtons[0].gameObject.SetActive(false);
            levels[i] = true;
        }
        else
        {
            outTransition.preview = previews[i];
            levelButtons[i].gameObject.SetActive(false);
            levels[i - 1] = true;
            i--;
        }

        StartCoroutine(outTransition.SwapTrack());

        inTransition.preview = previews[i];
        StartCoroutine(inTransition.StartTrack());
    }
}
