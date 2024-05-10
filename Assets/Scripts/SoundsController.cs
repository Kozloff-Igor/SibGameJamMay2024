using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClipType { Whales, CrystalAppear, CrystalMelody, CrystalFinal, FootSteps, HookThrow, HookGrab, HookTake, Cogs, Pipes, Water, MinigameDone }

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;
    public AudioSource loopingAmbient;
    public AudioSource loopingHook;
    public AudioSource playerSource;
    public AudioSource sourceInSpace;

    [System.Serializable]
    public struct Clips
    {
        public SoundClipType soundClipType;
        public AudioClip[] audioClips;
    }
    public Clips[] allClips;

    int[] clipsIndexes;

    void Awake()
    {        
        Instance = this;
        clipsIndexes = new int[allClips.Length];
        for (int q = 0; q < clipsIndexes.Length; q++)
        {
            for (int w = 0; w < allClips.Length; w++)
            {
                if ((int)allClips[w].soundClipType == q)
                {
                    clipsIndexes[q] = w;
                }
            }
        }
    }

    void Update()
    {

    }

    public void PlaySound(SoundClipType soundClipType)
    {
        int id = clipsIndexes[(int)soundClipType];
        AudioClip clipToPlay = allClips[id].audioClips[Random.Range(0, allClips[id].audioClips.Length)];
        playerSource.PlayOneShot(clipToPlay);
    }

    public void PlaySoundAtCertainPlace(SoundClipType soundClipType, Vector3 pos)
    {
        int id = clipsIndexes[(int)soundClipType];
        AudioClip clipToPlay = allClips[id].audioClips[Random.Range(0, allClips[id].audioClips.Length)];
        sourceInSpace.transform.position = pos;
        sourceInSpace.PlayOneShot(clipToPlay);
    }

    public void PlayHookLoop(bool isOn)
    {
        loopingHook.enabled = isOn;
    }
}