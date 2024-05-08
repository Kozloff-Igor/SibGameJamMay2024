using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{

    private AudioSource audioScr;
    private float musicVolume = 1f;
    
    void Start()
    {
        audioScr = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioScr.volume = musicVolume;
    }

    void SetVolume(float newvol)
    {
        musicVolume = newvol;
    }
}
