using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundClipType { Whales, CrystalAppear, CrystalMelody, CrystalFinal, FootSteps, HookThrow, HookGrab, HookTake, Cogs, Pipes, Water, MinigameDone }

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;
    public AudioSource loopingAmbient;

    public AudioSource planetMusic;
    public AudioSource loopingHook;
    public AudioSource playerSource;
    public AudioSource sourceInSpace;

    public Transform[] musicSourcesPlaces;
    Transform closestMusicTransform;

    public AudioClip[] musicClips;
    public Transform player;

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

    public bool gameIsNotFinished = true;

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
        StartCoroutine(FindClosestMusic());
    }

    void Update()
    {
        float dist = Vector3.Distance(closestMusicTransform.position, player.transform.position);
        if (dist < 30f) 
        {
//            closestMusic

        }


    }

    IEnumerator FindClosestMusic()
    {
        while (gameIsNotFinished)
        {
            float closestDist = Vector3.Distance(player.position, musicSourcesPlaces[0].transform.position);
            closestMusicTransform = musicSourcesPlaces[0];
            for (int q = 1; q < musicSourcesPlaces.Length; q++)
            {
                float dist = Vector3.Distance(player.position, musicSourcesPlaces[q].transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestMusicTransform = musicSourcesPlaces[q];
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
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
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(whalesCooldown.x, whalesCooldown.y));
            int id = clipsIndexes[(int)SoundClipType.Whales];
            AudioClip clipToPlay = allClips[id].audioClips[Random.Range(0, allClips[id].audioClips.Length)];
            loopingAmbient.PlayOneShot(clipToPlay, Random.Range(0.1f, 1f));
        }

    }

}