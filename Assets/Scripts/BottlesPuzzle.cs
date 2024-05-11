using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlesPuzzle : MonoBehaviour
{
    public static BottlesPuzzle Instance;
    public Color[] colors;

    BottleSingle currentBottle;
    public bool bottleIsSelected;
    [System.Serializable]
    public struct WhatsInBottle
    {
        public int freeSpace;
        public int topColor;
        public int howMuchTopColor;

    }

    public BottlesFlyingBall[] flyingBalls;
    public BottleSingle[] allBottles;

    void Awake()
    {
        Instance = this;
        QuestProgression.Instance.DisablePlayer();
    }

    WhatsInBottle BottleInfo(BottleSingle bottle)
    {
        WhatsInBottle whatsInBottle = new WhatsInBottle();
        int freeSpace = FreeSpace(bottle);
        whatsInBottle.freeSpace = freeSpace;
        if (freeSpace == bottle.myBalls.Length)
        {
            whatsInBottle.topColor = 0;
            whatsInBottle.howMuchTopColor = 4;
        }
        else
        {
            int topColor = bottle.myBalls[freeSpace];
            int howMuchTopColor = 0;
            bool stillTopColor = true;
            for (int q = freeSpace; q < bottle.myBalls.Length; q++)
            {
                if (bottle.myBalls[q] == topColor)
                {
                    if (stillTopColor) howMuchTopColor++;
                }
                else
                {
                    stillTopColor = false;
                }
            }
            whatsInBottle.topColor = topColor;
            whatsInBottle.howMuchTopColor = howMuchTopColor;
        }

        return whatsInBottle;
    }
    int FreeSpace(BottleSingle bottleSingle)
    {
        int free = 0;
        for (int q = 0; q < bottleSingle.myBalls.Length; q++)
        {
            if (bottleSingle.myBalls[q] > 0) return free;
            free++;
        }
        return free;
    }

    public void FillBottle(BottleSingle toFill)
    {
        WhatsInBottle sourceBottle = BottleInfo(currentBottle);
        WhatsInBottle fillableBottle = BottleInfo(toFill);
        int amount = sourceBottle.howMuchTopColor;
        int color = sourceBottle.topColor;
        if (fillableBottle.topColor == 0)
        {
            for (int q = sourceBottle.freeSpace; q < sourceBottle.freeSpace + amount; q++)
            {
                currentBottle.RemoveBall(q);
                BottlesFlyingBall flyingBall = GrabAvailableBall();
                int posId = toFill.myBalls.Length - 1 - q + sourceBottle.freeSpace;
                //toFill.AddBall(posId , color);
                flyingBall.Generate(color, currentBottle.myBallsImages[q].transform.position, toFill, posId);

            }
            for (int q = toFill.myBalls.Length - 1; q >= toFill.myBalls.Length - amount; q--)
            {
                //toFill.AddBall(q, color);
            }
        }
        else
        {
            if (fillableBottle.topColor == color)
            {
                if (fillableBottle.freeSpace >= sourceBottle.howMuchTopColor)
                {
                    for (int q = sourceBottle.freeSpace; q < sourceBottle.freeSpace + amount; q++)
                    {
                        currentBottle.RemoveBall(q);

                        BottlesFlyingBall flyingBall = GrabAvailableBall();
                        int posId = fillableBottle.freeSpace - 1 - q + sourceBottle.freeSpace;
                        //toFill.AddBall(posId, color);
                        flyingBall.Generate(color, currentBottle.myBallsImages[q].transform.position, toFill, posId);

                    }
                    for (int q = fillableBottle.freeSpace - 1; q >= fillableBottle.freeSpace - amount; q--)
                    {
                        //toFill.AddBall(q, color);
                    }
                }
            }

        }

        //if (sourceBottle.topColor == )
        DeselectBottle();

    }

    BottlesFlyingBall GrabAvailableBall()
    {
        for (int q = 0; q < flyingBalls.Length; q++)
        {
            if (!flyingBalls[q].isBusy)
            {
                flyingBalls[q].isBusy = true;
                return flyingBalls[q];
            }
        }
        return flyingBalls[0];
    }

    public bool SomeBallsAreFlying()
    {
        for (int q = 0; q < flyingBalls.Length; q++)
        {
            if (flyingBalls[q].isBusy) return true;
        }
        return false;
    }

    public void SelectBottle(BottleSingle bottleSingle)
    {
        currentBottle = bottleSingle;
        bottleIsSelected = true;
        SoundsController.Instance.PlaySound(SoundClipType.Water);
    }

    public void DeselectBottle()
    {
        if (currentBottle != null) { currentBottle.isSelected = false; }
        currentBottle = null;
        bottleIsSelected = false;
        SoundsController.Instance.PlaySound(SoundClipType.Water);
        CheckIfPuzzleSolved();
    }

    void CheckIfPuzzleSolved()
    {
        int goodBottlesFound = 0;
        for (int q = 0; q < allBottles.Length; q++)
        {
            WhatsInBottle whatsInBottle = BottleInfo(allBottles[q]);
            if (whatsInBottle.freeSpace == 0)
            {
                if (whatsInBottle.howMuchTopColor == 4)
                {
                    goodBottlesFound++;
                }
            }

        }

        if (goodBottlesFound == 7)
        {
            SoundsController.Instance.PlaySound(SoundClipType.MinigameDone);
            QuestProgression.Instance.FinishTheGame();
        }

        Debug.Log(goodBottlesFound.ToString());
    }

    public void BTN_Restart()
    {
        if (SomeBallsAreFlying()) return;
        currentBottle = null;
        bottleIsSelected = false;
        for (int i = 0; i < allBottles.Length; i++)
        {            
            allBottles[i].Restart();
        }

    }

}
