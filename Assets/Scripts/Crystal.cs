using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IInteractable
{
    
    [SerializeField]private GameObject crystal;
    [SerializeField]private Vector3 spawnCrystal;
    private int CountCrystal = 1;
    public float speed = 0.2f;
    public float valueGrow = 0.25f;
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
       // Debug.Log(this.transform.position.y);
        StartCoroutine(Grow());
        CountCrystal++;
    }

    IEnumerator Grow()
    {
        while (crystal.transform.position.y < this.transform.position.y)
        {
            spawnCrystal = crystal.transform.position;
            spawnCrystal.y += valueGrow;
            crystal.transform.position = spawnCrystal;
            yield return new WaitForSeconds(speed);
        }
        
        
    }
    
}
