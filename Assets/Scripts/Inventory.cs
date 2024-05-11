using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]private GameObject panelNotesAndPicture;
    private void OnDisable()
    {
        Test.OpenInventory -= OpenInventory;
    }

    private void OnEnable()
    {
        Test.OpenInventory += OpenInventory;
    }

    void OpenInventory()
    {
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           panelNotesAndPicture.SetActive(false);
        }
    }
}
