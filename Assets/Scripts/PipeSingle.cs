using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSingle : MonoBehaviour
{
    public int myDirection;
    public int myCorrectDirection;
    public int totalDirectionsCount = 2;
    public Transform pipeImage;
    public bool needCheck;
    
    void Awake(){
        pipeImage.localEulerAngles = new Vector3(0,0,90f * myDirection);
    }

    public void BTN_Click(){
        myDirection += 3;
        myDirection %= totalDirectionsCount;
        pipeImage.localEulerAngles = new Vector3(0,0,90f * myDirection);
        SoundsController.Instance.PlaySound(SoundClipType.Pipes);
        PipesPuzzle.Instance.CheckCorrectness();
    }

}
