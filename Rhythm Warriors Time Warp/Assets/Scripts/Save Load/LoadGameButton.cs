using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameButton : MonoBehaviour
{
    public SaveLoadUI[] saveloadUI;

    public GameObject[] slots;
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject slot in slots)
        {
            if (slots[0].activeSelf)
            {
                btn.onClick.AddListener(() => saveloadUI[0].LoadSlot1Clicked());
            }
            else if (slots[1].activeSelf)
            {
                btn.onClick.AddListener(() => saveloadUI[1].LoadSlot2Clicked());
            }
            else if (slots[2].activeSelf)
            {
                btn.onClick.AddListener(() => saveloadUI[2].LoadSlot3Clicked());
            }
        }
    }
}
