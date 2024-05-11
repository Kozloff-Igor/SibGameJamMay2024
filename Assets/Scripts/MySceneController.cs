using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneController : MonoBehaviour
{
    public Image blackImage;
    Color startingColor = new Color(0,0,0,0);
    public AudioSource music;    

    public void BTN_OpenScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void BTN_OpenSceneAfterBlackingOut(int sceneId)
    {
        StartCoroutine(BlackingOut(sceneId));
    }

    IEnumerator BlackingOut(int sceneId)
    {
        SoundsController soundsController = FindObjectOfType<SoundsController>();
        if (soundsController) soundsController.enabled = false;

        blackImage.gameObject.SetActive(true);        
        
        float z = 0f;
        while (z < 2f)
        {
            z += Time.deltaTime;
            blackImage.color = Color.Lerp(startingColor, Color.black, z / 2f);
            music.volume = Mathf.MoveTowards(music.volume, 0f, Time.deltaTime * 0.5f);
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(sceneId);
    }


}
