using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesPuzzle : MonoBehaviour
{
    public static PipesPuzzle Instance;
    List<PipeSingle> pipesToCheck;
    bool puzzleIsFinished;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        QuestProgression.Instance.DisablePlayer();
        pipesToCheck = new List<PipeSingle>();
        PipeSingle[] pipeSingles = FindObjectsOfType<PipeSingle>();
        for (int q = 0; q < pipeSingles.Length; q++)
        {
            if (pipeSingles[q].needCheck)
            {
                pipesToCheck.Add(pipeSingles[q]);
            }

        }
    }



    public void CheckCorrectness()
    {
        bool everythingFine = true;
        for (int q = 0; q < pipesToCheck.Count; q++)
        {
            if (pipesToCheck[q].myDirection != pipesToCheck[q].myCorrectDirection)
            {
                everythingFine = false;
            }
        }
        if (everythingFine)
        {
            if (puzzleIsFinished) return;
            puzzleIsFinished = true;
            StartCoroutine(PuzzleCompleted());


        }

    }


    IEnumerator PuzzleCompleted()
    {
        SoundsController.Instance.PlaySound(SoundClipType.MinigameDone);
        float lerpValue = 1;
        for (int i = 0; i < 3; i++)
        {
            while (lerpValue < 1.3f)
            {
                lerpValue += Time.deltaTime * 1f;
                for (int q = 0; q < pipesToCheck.Count; q++)
                {
                    pipesToCheck[q].transform.localScale = new Vector3(lerpValue, lerpValue, lerpValue);
                }
                yield return new WaitForEndOfFrame();
            }
            while (lerpValue > 1f)
            {
                lerpValue -= Time.deltaTime * 1f;
                for (int q = 0; q < pipesToCheck.Count; q++)
                {
                    pipesToCheck[q].transform.localScale = new Vector3(lerpValue, lerpValue, lerpValue);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        QuestProgression.Instance.CollectStrangeWater();
        QuestProgression.Instance.EnablePlayer();
        gameObject.SetActive(false);
    }

}
