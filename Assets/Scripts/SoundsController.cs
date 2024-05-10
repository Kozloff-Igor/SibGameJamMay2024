<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum SoundClipType { Whales, CrystalAppear, CrystalMelody, CrystalFinal, FootSteps, HookThrow, HookGrab, HookTake, Cogs, Pipes, Water, MinigameDone }

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;
    public AudioSource loopingAmbient;
    public AudioSource planetMusic;
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

    [Header("_____")]
    public Vector2 whalesCooldown;


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

        StartCoroutine(WhailingWhales());
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

    IEnumerator WhailingWhales()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(whalesCooldown.x, whalesCooldown.y));
            int id = clipsIndexes[(int)SoundClipType.Whales];
            AudioClip clipToPlay = allClips[id].audioClips[Random.Range(0, allClips[id].audioClips.Length)];
            loopingAmbient.PlayOneShot(clipToPlay, Random.Range(0.1f, 1f));
        }

    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum SoundClipType { Whales, CrystalAppear, CrystalMelody, CrystalFinal, FootSteps, HookThrow, HookGrab, HookTake, Cogs, Pipes, Water, MinigameDone }

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;
    public AudioSource loopingAmbient;
    public AudioSource planetMusic;
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

    [Header("_____")]
    public Vector2 whalesCooldown;


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

        StartCoroutine(WhailingWhales());
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

    IEnumerator WhailingWhales()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(whalesCooldown.x, whalesCooldown.y));
            int id = clipsIndexes[(int)SoundClipType.Whales];
            AudioClip clipToPlay = allClips[id].audioClips[Random.Range(0, allClips[id].audioClips.Length)];
            loopingAmbient.PlayOneShot(clipToPlay, Random.Range(0.1f, 1f));
        }

    }
>>>>>>> Stashed changes
}