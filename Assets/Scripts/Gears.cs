using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    private GameObject gears;
    private float speed;

    void SetActive()
    {
        gears.SetActive(true);
    }

    public void FixedUpdate()
    {
        gears.transform.Rotate(0f, 0f,speed);
    }
}
