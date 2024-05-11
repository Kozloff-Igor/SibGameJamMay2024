using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour, IInteractable
{
    public GameObject needToExplore; //is broken, go explore
    public GameObject haveCrystals; //
    public GameObject haveCogs;
    public GameObject haveCrystalDust;
    


    public GameObject pipesCanvas;

    public void OnInteraction()
    {       
        if (QuestProgression.Instance.haveStrangeWater)
        {
            //QuestProgression.ShowObjectForThreeSeconds(alreadyHaveStrangeWater);
            return;
        }
        Debug.Log("Start pipes game");
        pipesCanvas.SetActive(true);
        

    }
}
