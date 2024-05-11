using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, IInteractable
{
public GameObject alreadyHaveStrangeWater;

    public GameObject pipesCanvas;

    public void OnInteraction()
    {       
        if (QuestProgression.Instance.haveStrangeWater)
        {
            QuestProgression.ShowObjectForThreeSeconds(alreadyHaveStrangeWater);
            return;
        }
        Debug.Log("Start pipes game");
        pipesCanvas.SetActive(true);
        

    }

}
