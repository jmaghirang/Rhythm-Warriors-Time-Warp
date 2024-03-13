using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadToggles : SettingsTabs
{
    public List<Toggle> slots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int t = i;
            slots[t].onValueChanged.AddListener(x => DisplayContent(contents[t]));
        }

        back.onClick.AddListener(() => gameObject.SetActive(false));
        back.onClick.AddListener(() => pauseScreen.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
