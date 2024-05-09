using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]private Button buttonPlay;
    [SerializeField]private Button buttonExit;
    [SerializeField]private Button buttonSettings;
    [SerializeField]private Button buttonInventory;


    [SerializeField]private GameObject settingsMenu;
    [SerializeField]private GameObject notesAndPicture;

    //настройки
    public void ClickSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        Debug.Log("" + settingsMenu.activeSelf);
    }

    //играть
    public void ClickPlayGame()
    {
        settingsMenu.SetActive(false);
        Test.LoadScene();
    }
    
    public void ClickInventory()
    {
        settingsMenu.SetActive(false);
        notesAndPicture.SetActive(true);
        Test.OpenInventory?.Invoke();
    }
    //выход
    public void ClickQuit()
    {
        Application.Quit();
    }
    
    private void OnEnable()
    {
        buttonExit.onClick.AddListener(ClickQuit);
        buttonPlay.onClick.AddListener(ClickPlayGame);
        buttonSettings.onClick.AddListener(ClickSettings);
        buttonInventory.onClick.AddListener(ClickInventory);
    }

    private void OnDisable()
    {
        buttonExit.onClick.RemoveListener(ClickQuit);
        buttonPlay.onClick.RemoveListener(ClickPlayGame);
        buttonSettings.onClick.RemoveListener(ClickSettings);
        buttonInventory.onClick.RemoveListener(ClickInventory);
        
    }
}
