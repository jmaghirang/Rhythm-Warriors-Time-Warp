using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtons : SettingsTabs
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            int t = i;
            tabs[t].onClick.AddListener(() => DisplayContent(contents[t]));
            tabs[t].onClick.AddListener(() => gameObject.SetActive(false));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
