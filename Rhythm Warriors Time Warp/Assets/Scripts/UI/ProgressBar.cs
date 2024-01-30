using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://youtu.be/J1ng1zA3-Pk?si=xuNBh0Qskzrip85-

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public double min;
    public double max;
    public double current;
    public Image mask;
    public Image fill;
    public Color color;

    void Update()
    {
        GetCurrentFill();
    }

    public void GetCurrentFill()
    {
        float currentOffset = (float) (current - min);
        float maximumOffset = (float) (max - min);
        float fillAmount = currentOffset / maximumOffset;

        mask.fillAmount = fillAmount;
        fill.color = color;
    }
}
