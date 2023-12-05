using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRestrictions : MonoBehaviour
{
    public List<Button> disabledButtons;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
