using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

//code from https://youtu.be/irJm8LkkDGw?si=X4xQS6N764VAtp2Q

public class ShowKeyboard : MonoBehaviour
{
    public static ShowKeyboard instance;

    public TMP_InputField inputField;

    public Button confirm;

    public float distance = 0.5f;
    public float verticalOffset = -0.5f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());

        inputField.characterLimit = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        Vector3 direction = GameManager.instance.player.transform.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = GameManager.instance.player.transform.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);

        SetCaretColorAlpha(1);

        confirm.gameObject.SetActive(false);

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);

        confirm.gameObject.SetActive(true);

        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;
        inputField.caretColor = caretColor;
    }
}
