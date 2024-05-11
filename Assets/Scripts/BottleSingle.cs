using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleSingle : MonoBehaviour
{
    public int[] myBalls;
    public Image[] myBallsImages;
    public bool isSelected;

    public int[] initialBalls;

    public void BTN_CLick()
    {
        if (BottlesPuzzle.Instance.SomeBallsAreFlying()) return;
        if (isSelected)
        {
            isSelected = false;
            BottlesPuzzle.Instance.DeselectBottle();
            return;
        }
        if (BottlesPuzzle.Instance.bottleIsSelected)
        {
            BottlesPuzzle.Instance.FillBottle(this);
        }
        else
        {
            if (myBalls[3] > 0) //can't select empty bottle
            {
                isSelected = true;
                BottlesPuzzle.Instance.SelectBottle(this);
            }
        }

    }

    public void RemoveBall(int position)
    {
        myBalls[position] = 0;
        myBallsImages[position].color = BottlesPuzzle.Instance.colors[0];
    }

    public void AddBall(int position, int color)
    {
        myBalls[position] = color;
        myBallsImages[position].color = BottlesPuzzle.Instance.colors[color];
    }

    public void Restart()
    {
        for (int i = 0; i < myBalls.Length; i++)
        {
            myBalls[i] = initialBalls[i];
            myBallsImages[i].color = BottlesPuzzle.Instance.colors[myBalls[i]];            
        }
        isSelected = false;
        Debug.Log("RESTARTED");
    }

    void Start()
    {
        initialBalls = new int[myBalls.Length];
        for (int i = 0; i < myBalls.Length; i++)
        {
            myBallsImages[i].color = BottlesPuzzle.Instance.colors[myBalls[i]];
            initialBalls[i] = myBalls[i];
        }

    }

    void Update()
    {
        if (isSelected)
        {
            transform.localScale = Vector3.one * (1 + Mathf.Sin(Time.time * 16) * 0.1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

    }


}
