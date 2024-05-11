using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour, IInteractable
{
    public GameObject textNeedCrystals;
    public GameObject needCogs;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteraction()
    {
        if (!QuestProgression.Instance.haveCollectedCrystals)
        {
            QuestProgression.ShowObjectForThreeSeconds(textNeedCrystals);
            return;
        }
        if (QuestProgression.Instance.cogsCollected < QuestProgression.Instance.cogsRequired)
        {
            QuestProgression.ShowObjectForThreeSeconds(needCogs);
            return;
        }
        Debug.Log("Start cogs game");
        QuestProgression.Instance.CollectCrystalsDust();   


    }



}
