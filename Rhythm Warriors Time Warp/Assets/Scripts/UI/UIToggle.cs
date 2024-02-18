using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIToggle : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // we'll add a section for this under the settings menu
    private bool uiCustomizationMode = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiCustomizationMode)
        {
            // handle UI customization mode
            // implement logic to allow dragging and rearranging UI elements
        }
    }

    public void SetUICustomizationMode(bool mode)
    {
        uiCustomizationMode = mode;

        // enable or disable interaction based on mode
        if (mode)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
    }
}