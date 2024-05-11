using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestProgression : MonoBehaviour
{
    public static QuestProgression Instance;

    public bool haveCollectedCrystals;
    public int cogsCollected;
    public int cogsRequired;
    public bool haveCrystalDust;
    public bool haveStrangeWater;

    [Header("UI stuff")]
    public GameObject crystalsIcon;
    public GameObject crystalsDustIcon;
    public GameObject cogsIcon;
    public GameObject strangeWaterIcon;
    public TMP_Text cogsCollectedText;



    FirstPersonController firstPersonController;
    Hook hook;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        firstPersonController = FindObjectOfType<FirstPersonController>();
        playerRb = firstPersonController.GetComponent<Rigidbody>();
        hook = FindObjectOfType<Hook>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisablePlayer();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnablePlayer();
        }*/

    }

    public void CollectCrystals()
    {
        if (!haveCollectedCrystals)
        {
            crystalsIcon.SetActive(true);
            SoundsController.Instance.PlaySound(SoundClipType.CrystalFinal);
        }
        haveCollectedCrystals = true;
    }

    public void CollectCog()
    {
        cogsCollected++;
        cogsCollectedText.text = cogsCollected.ToString();
        cogsIcon.SetActive(true);
        SoundsController.Instance.PlaySound(SoundClipType.Cogs);
    }
    public void CollectCrystalsDust()
    {
        if (haveCrystalDust) return;
        haveCrystalDust = true;
        cogsIcon.SetActive(false);
        crystalsIcon.SetActive(false);
        crystalsDustIcon.SetActive(true);
    }

    public void CollectStrangeWater()
    {
        if (haveStrangeWater) return;
        haveStrangeWater = true;
        strangeWaterIcon.SetActive(true);

    }



    public void DisablePlayer()
    {
        firstPersonController.enabled = false;
        playerRb.isKinematic = true;
        hook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnablePlayer()
    {
        firstPersonController.enabled = true;
        playerRb.isKinematic = false;
        hook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void ShowObjectForThreeSeconds(GameObject g)
    {
        Instance.ShowObjectForTime(g, 3f);
    }

    public void ShowObjectForTime(GameObject g, float timer)
    {
        StartCoroutine(ShowObjectAndHideLater(g, timer));
    }

    IEnumerator ShowObjectAndHideLater(GameObject g, float timer)
    {
        g.SetActive(true);
        yield return new WaitForSeconds(timer);
        g.SetActive(false);
    }
}
