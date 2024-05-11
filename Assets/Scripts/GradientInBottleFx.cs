using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientInBottleFx : MonoBehaviour
{
    public Gradient gradient;

    float c;
    public Image image;

    void Update()
    {
        c += Time.deltaTime * 0.03f;
        c %= 1f;
        image.color = gradient.Evaluate(c);
    }
}
