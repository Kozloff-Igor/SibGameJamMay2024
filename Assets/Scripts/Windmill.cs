using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour, IInteractable
{
    public GameObject textNeedCrystals;
    public GameObject needCogs;
    public GameObject alreadyHaveDust;

    public GameObject cogCanvas;
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
        if (QuestProgression.Instance.haveCrystalDust)
        {
            QuestProgression.ShowObjectForThreeSeconds(alreadyHaveDust);
            return;
        }
        Debug.Log("Start cogs game");
        cogCanvas.SetActive(true);
        //QuestProgression.Instance.CollectCrystalsDust();   


    }



}
