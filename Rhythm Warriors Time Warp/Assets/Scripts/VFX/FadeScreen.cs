using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//https://youtu.be/JCyJ26cIM0Y?si=bf14vEAU_-TZafnZ

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 3;
    public Color fadeInColor;
    public Color fadeOutColor;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(fadeInColor, 1, 0);
    }

    public void FadeOut()
    {
        Fade(fadeOutColor, 0, 1);
    }

    public void Fade(Color fadeColor, float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(fadeColor, alphaIn, alphaOut));
    }

    IEnumerator FadeRoutine(Color fadeColor, float alphaIn, float alphaOut)
    {
        float timer = 0;
        // How long fade will last
        while (timer <= fadeDuration)
        {
            Color fColor = fadeColor;

            // Adjust alpha value of color from alphaIn to alphaOut at a rate timer / fadeDuration
            fColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);

            rend.material.SetColor("_BaseColor", fColor);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final value of alpha is alphaOut
        Color alphaOutColor = fadeColor;
        alphaOutColor.a = alphaOut;
        rend.material.SetColor("_BaseColor", alphaOutColor);
    }
}
