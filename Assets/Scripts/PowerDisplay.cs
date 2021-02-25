using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerDisplay : MonoBehaviour
{
    public Slider bar;
    public Image fill;
    public Gradient gradient;

    public void SetPowerBar(float p)
    {
        bar.value = p;
        fill.color = gradient.Evaluate(p);
    }
}
