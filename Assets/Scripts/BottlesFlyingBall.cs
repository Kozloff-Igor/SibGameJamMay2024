using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottlesFlyingBall : MonoBehaviour
{
    public bool isBusy;
    public Transform topWall;
    Vector3 targetPos;
    Vector3 localTargetPos;
    enum State { flyingUp, flyingHorizontal, flyingDown }
    State myState;
    public Image image;
    BottleSingle targetBottle;
    int myTargetPlaceId;
    int myColorId;
    float speed = 1000f;



    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, localTargetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, localTargetPos) <= 0.001f)
        {
            if (myState == State.flyingUp)
            {
                myState = State.flyingHorizontal;
                localTargetPos = new Vector3(targetPos.x, topWall.position.y, topWall.position.z);
            }
            else
            {
                if (myState == State.flyingHorizontal)
                {
                    myState = State.flyingDown;
                    localTargetPos = targetPos;
                }
                else
                {
                    FinishedFlying();
                }
            }
        }
    }

    public void Generate(int colorId, Vector3 startPosition, BottleSingle targetBottleSingle, int myPlaceId)
    {
        isBusy = true;
        targetBottle = targetBottleSingle;

        transform.position = startPosition;
        targetPos = targetBottle.myBallsImages[myPlaceId].transform.position;
        myState = State.flyingUp;
        image.color = BottlesPuzzle.Instance.colors[colorId];

        myTargetPlaceId = myPlaceId;
        myColorId = colorId;

        localTargetPos = new Vector3(startPosition.x, topWall.position.y, topWall.position.z);

        gameObject.SetActive(true);
    }

    public void FinishedFlying()
    {
        targetBottle.AddBall(myTargetPlaceId, myColorId);
        isBusy = false;
    }

}
