using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IInteractable
{

    [SerializeField] private Crystal crystalNext;

    [SerializeField] private bool firstCrystal;
    [SerializeField] private bool lastCrystal;
    bool alreadyUsed;
    Vector3 targetPosition;
    Quaternion targetRotation;
    public float speed = 0.2f;


    void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
        if (!firstCrystal)
        {
            transform.Translate(Vector3.down * 3f, Space.Self);
            transform.Rotate(0, 179, 0, Space.Self);
        }
    }

    public void OnInteraction()
    {
        Debug.Log("Player pressed E on me", gameObject);

        if (alreadyUsed)
        {
            SoundsController.Instance.PlaySound(SoundClipType.CrystalMelody);
            return;
        }

        alreadyUsed = true;

        if (!lastCrystal)
        {
            crystalNext.GrowCrystal();
        } else{            
            Debug.Log("Got crystals");
            QuestProgression.Instance.CollectCrystals();
        }

    }
    public void GrowCrystal()
    {
        // Debug.Log(this.transform.position.y);
        SoundsController.Instance.PlaySoundAtCertainPlace(SoundClipType.CrystalAppear, transform.position);
        StartCoroutine(Grow());

    }

    IEnumerator Grow()
    {
        while (Vector3.SqrMagnitude(transform.position - targetPosition) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30f * Time.deltaTime);
            yield return new WaitForEndOfFrame();


            //yield return new WaitForSeconds(0.1f);
        }


    }

}
