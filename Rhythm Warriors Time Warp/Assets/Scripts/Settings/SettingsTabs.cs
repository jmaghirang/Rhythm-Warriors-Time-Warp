//using Nova;
//using NovaSamples.UIControls;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsTabs : MonoBehaviour
{
    public List<Button> tabs;
    public List<GameObject> contents;

    public GameObject pauseScreen;
    public Button back;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int t = i;
            tabs[t].onClick.AddListener(() => DisplayContent(contents[t]));
        }

        back.onClick.AddListener(() => gameObject.SetActive(false));
        back.onClick.AddListener(() => pauseScreen.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayContent(GameObject content)
    {
        foreach (GameObject block in contents)
        {
            block.SetActive(false);
        }

        content.SetActive(true);
    }
}
