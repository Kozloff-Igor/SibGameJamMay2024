using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSpawn : MonoBehaviour
{
    public static List<Crystal> crystalGroup;
    
    
    // private void OnEnable()
    // {
    //     Test.ClickCrystal += GrowCrystal;
    // }
    //
    // private void OnDisable()
    // {
    //     Test.ClickCrystal -= GrowCrystal;
    //
    // }

    // private void GrowCrystal()
    // {
    //     for (int i = 0; i < this.transform.childCount; i++)
    //     {
    //         if (this.transform.GetChild(i).GetComponent<Crystal>() != null)
    //         {
    //             crystalGroup.Add(this.transform.GetChild(i).GetComponent<Crystal>());
    //             
    //         }
    //     }
    //     
    //     crystalGroup[Crystal.CountCrystal + 1].SpawnCrystal();
    //     crystalGroup[Crystal.CountCrystal-1].canSpawn = false;
    // }
}
