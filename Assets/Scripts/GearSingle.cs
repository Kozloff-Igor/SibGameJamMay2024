using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearSingle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public enum Size { none, small, medium, big, huge }
    public Size size;
    public bool isMovable;
    public bool curentlyRotating;

    public float rotationDirection = 1f;

    RectTransform rectTransform;
    Image image;
    Transform myPin;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        myPin = transform.parent;
    }


    void Update()
    {
        if (curentlyRotating)
        {
            transform.Rotate(0, 0, 90f * Time.deltaTime * (5 - (int)size) * rotationDirection);
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isMovable) return;
        myPin.GetComponent<GearPin>().myCurrentGear = null;
        CheckPins();        
        transform.SetParent(GearsPuzzle.Instance.parentForDrags);
        curentlyRotating = false;
        SoundsController.Instance.PlaySound(SoundClipType.Cogs);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMovable) return;

        //rectTransform.anchoredPosition += eventData.delta;
        CheckPins();

    }

    void CheckPins(bool isEndDrag = false)
    {
        transform.position = Input.mousePosition;
        float sqrDist = 0;
        GearPin closestPin = GearsPuzzle.Instance.ClosestPin(transform.position, out sqrDist);

        if (sqrDist < 1000f)
        {
            bool canBePlaced = closestPin.CheckIfICanBePlaced(this);
            Debug.Log(closestPin);
            if (canBePlaced)
            {
                image.color = Color.green;
                if (isEndDrag)
                {
                    myPin = closestPin.transform;                    
                }
            }
            else
            {
                image.color = Color.red;
            }
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0.5f);

        }
        if (isEndDrag)
        {
            transform.SetParent(myPin);
            GearPin myGearPin = myPin.GetComponent<GearPin>();
            myGearPin.myCurrentGear = this;
            if (myGearPin.isClockwise) rotationDirection = 1f; else rotationDirection = -1f;
            transform.position = myPin.transform.position;
        }

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isMovable) return;
        CheckPins(true);
        image.color = new Color(1f, 1f, 1f, 1f);
        SoundsController.Instance.PlaySound(SoundClipType.Cogs);        
    }

}
