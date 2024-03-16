using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Offset : MonoBehaviour
{
    public Button increase;
    public Button decrease;

    public TextMeshProUGUI offsetInputField;

    public int offsetValue = 0; 

    // Start is called before the first frame update
    void Start()
    {
        offsetInputField.text = offsetValue.ToString();

        increase.onClick.AddListener(() => IncreaseMS());
        decrease.onClick.AddListener(() => DecreaseMS());
    }

    public void IncreaseMS()
    {
        offsetValue += 5;
        offsetInputField.text = offsetValue.ToString();
    }

    public void DecreaseMS()
    {
        offsetValue -= 5;
        offsetInputField.text = offsetValue.ToString();
    }
}
