using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearsPuzzle : MonoBehaviour
{
    public static GearsPuzzle Instance;
    public GearPin[] gearPins;
    public Transform[] allPinsTr;
    public Transform parentForDrags;

    public GearSingle finalGear;
    public Image finalGearImage;
    bool puzzleIsFinished = false;
    Color initialFinalCogColor;
    

    void Start()
    {
        Instance = this;
        QuestProgression.Instance.DisablePlayer();
        initialFinalCogColor = finalGearImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (finalGear.curentlyRotating)
        {
            if (puzzleIsFinished) return;
            puzzleIsFinished = true;
            StartCoroutine(PuzzleCompleted());

        }
    }

    IEnumerator PuzzleCompleted()
    {
        SoundsController.Instance.PlaySound(SoundClipType.MinigameDone);
        float lerpValue = 0;
        while (lerpValue < 1.3f)
        {
            lerpValue += Time.deltaTime * 0.3f;
            finalGearImage.color = Color.Lerp(initialFinalCogColor, Color.green, lerpValue);
            yield return new WaitForEndOfFrame();
        }
        QuestProgression.Instance.CollectCrystalsDust();
        QuestProgression.Instance.EnablePlayer();
        gameObject.SetActive(false);
    }

    public GearPin ClosestPin(Vector3 pos, out float closestDist)
    {
        closestDist = Vector3.SqrMagnitude(pos - allPinsTr[0].position);
        int closestId = 0;
        for (int q = 1; q < allPinsTr.Length; q++)
        {
            float dist = Vector3.SqrMagnitude(pos - allPinsTr[q].position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestId = q;
            }
        }        
        return gearPins[closestId];
    }

}
