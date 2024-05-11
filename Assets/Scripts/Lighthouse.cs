using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour, IInteractable
{
    public GameObject needToExplore; //is broken, go explore   
    public GameObject stoneReminder; //stone was good 
    public GameObject haveCrystals; //have to mill them
    public GameObject haveCogs; //can repair mill
    public GameObject haveCrystalDust; // now i need magic water
    
    public GameObject bottlesCanvas;

    public void OnInteraction()
    {    
        if (!(QuestProgression.Instance.haveCollectedCrystals || QuestProgression.Instance.cogsCollected > 0 ||
        QuestProgression.Instance.haveStrangeWater || QuestProgression.Instance.haveCrystalDust || QuestProgression.Instance.visitedStone)) {
            QuestProgression.ShowObjectForThreeSeconds(needToExplore);
            return;
        }

        if (QuestProgression.Instance.haveStrangeWater && QuestProgression.Instance.haveCrystalDust) 
        {
            bottlesCanvas.SetActive(true);
            return;
        }
        
        if (QuestProgression.Instance.haveCrystalDust)
        {
            QuestProgression.ShowObjectForThreeSeconds(haveCrystalDust);
            return;
        }

        if (QuestProgression.Instance.cogsCollected > 0)
        {
            QuestProgression.ShowObjectForThreeSeconds(haveCogs);
            return;
        }

        if (QuestProgression.Instance.haveCollectedCrystals)
        {
            QuestProgression.ShowObjectForThreeSeconds(haveCrystals);
            return;
        }
        
        if (QuestProgression.Instance.visitedStone)
        {
            QuestProgression.ShowObjectForThreeSeconds(stoneReminder);
            return;
        }
        

    }
}
