using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IInteractable
{
    
    [SerializeField]private GameObject crystal;
    [SerializeField]private Vector3 spawnCrystal;
    private int CountCrystal = 1;
    public int btwbcrystals = 5;
    public void OnInteraction()
    {
        Debug.Log("Player pressed E on me", gameObject);

        if (CountCrystal < 5)
        {
            GrowCrystal();
        }
        
    }
    void GrowCrystal()
    {
        spawnCrystal = crystal.transform.position;
        spawnCrystal.y += btwbcrystals;
        crystal.transform.position = spawnCrystal;
        CountCrystal++;
    }
}
