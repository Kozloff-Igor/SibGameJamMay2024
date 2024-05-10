using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Gears : MonoBehaviour
{
    [SerializeField]private GameObject gears;
    [SerializeField]private float speed = 5;

    [SerializeField]private bool missingears;
    [SerializeField]private bool clockwise;
    [SerializeField]private float valuesize;


    private bool move;
    private Vector3 MousePos;


    private void OnMouseDown()
    {
        if (missingears)
        {
            move = true;
        }
    }

    void OnMouseUp()
    {
        move = false;
    }

    private void Update()
    {
        MousePos = Input.mousePosition;
        
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        MousePos.z = 0;
        if (move)
        {
            this.transform.position = MousePos;
        }
    }

    // public void FixedUpdate()
    // {
    //     if (clockwise)
    //     {
    //         gears.transform.Rotate(0f, 0f,speed/valuesize);
    //     }
    //     else
    //     {
    //         gears.transform.Rotate(0f, 0f,-speed/valuesize);
    //
    //     }
    // }
}
