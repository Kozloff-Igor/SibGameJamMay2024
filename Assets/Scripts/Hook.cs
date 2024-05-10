using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hook : MonoBehaviour
{
    Transform player;
    Rigidbody playerRb;
    Transform hookTr;
    public Transform hookGunPoint;
    Camera cam;
    float minDotAngle = 0.95f;
    float curDotAngle;
    float hookSpeed = 60f;
    float playerRetractingSpeed = 35f;
    float hookDist = 50f;
    float hookDistSqr;
    List<Transform> hookableAsteroids;
    List<Transform> hookablePlanets;
    bool hookTargetExists;
    Transform currentHookablePoint;
    Transform grabbedToThis;
    public Transform virtualPointOnPlanet;
    public enum HookBehavior { InHand, FlyingToGrab, Grabbed, RetractingWithPlayer, Retracting }
    public HookBehavior hookBehavior;
    int checksAmountPerIteration = 10;
    public LayerMask planetsAndAsteroids;
    public LayerMask planetsOnly;
    public Transform crosshair;

    void Start()
    {
        cam = Camera.main;
        player = cam.transform;
        playerRb = FindObjectOfType<FirstPersonController>().GetComponent<Rigidbody>();
        hookTr = transform;

        hookDistSqr = hookDist * hookDist;
        GameObject[] g = GameObject.FindGameObjectsWithTag("Hookable");
        hookableAsteroids = new List<Transform>();
        for (int i = 0; i < g.Length; i++)
        {
            hookableAsteroids.Add(g[i].transform);
        }
        StartCoroutine(FindPointToGrab());
    }

    // Update is called once per frame
    void Update()
    {

        if (hookTargetExists)
        {
            crosshair.position = cam.WorldToScreenPoint(currentHookablePoint.position);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hookTargetExists)
            {
                if (grabbedToThis == currentHookablePoint) return;
                grabbedToThis = currentHookablePoint;
                hookBehavior = HookBehavior.FlyingToGrab;
                SoundsController.Instance.PlaySound(SoundClipType.HookThrow);
                SoundsController.Instance.PlayHookLoop(false);
                hookTr.position = hookGunPoint.position;
                hookTr.SetParent(null);
            }
            else
            {
                RaycastHit hit;
                Ray ray = new Ray(player.position, player.forward);
                bool hitPlanet = Physics.Raycast(ray, out hit, hookDist, planetsOnly);
                if (hitPlanet)
                {
                    virtualPointOnPlanet.position = hit.point;
                    grabbedToThis = virtualPointOnPlanet;
                    hookBehavior = HookBehavior.FlyingToGrab;
                    SoundsController.Instance.PlaySound(SoundClipType.HookThrow);                    
                    SoundsController.Instance.PlayHookLoop(false);
                    hookTr.position = hookGunPoint.position;
                    hookTr.SetParent(null);
                }
            }
        }


        if (Input.GetMouseButton(1))
        {
            if (hookBehavior == HookBehavior.Grabbed)
            {
                hookBehavior = HookBehavior.RetractingWithPlayer;
                SoundsController.Instance.PlayHookLoop(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hookBehavior != HookBehavior.InHand)
            {
                hookBehavior = HookBehavior.Retracting;
                SoundsController.Instance.PlayHookLoop(true);
            }
        }

    }

    void FixedUpdate()
    {
        if (hookBehavior == HookBehavior.InHand) return;
        if (hookBehavior == HookBehavior.FlyingToGrab)
        {
            hookTr.position = Vector3.MoveTowards(hookTr.position, grabbedToThis.position, hookSpeed * Time.deltaTime);
            if (Vector3.SqrMagnitude(hookTr.position - grabbedToThis.position) < 0.001f)
            {
                hookBehavior = HookBehavior.Grabbed;
                SoundsController.Instance.PlayHookLoop(false);
                SoundsController.Instance.PlaySoundAtCertainPlace(SoundClipType.HookGrab, hookTr.position);
            }
        }
        if (hookBehavior == HookBehavior.Grabbed) return;
        if (hookBehavior == HookBehavior.Retracting)
        {
            hookTr.position = Vector3.MoveTowards(hookTr.position, hookGunPoint.position, hookSpeed * Time.deltaTime);
            if (Vector3.SqrMagnitude(hookTr.position - hookGunPoint.position) < 0.001f)
            {
                hookBehavior = HookBehavior.InHand;
                SoundsController.Instance.PlayHookLoop(false);
                SoundsController.Instance.PlaySound(SoundClipType.HookTake);                
                hookTr.SetParent(hookGunPoint);
                grabbedToThis = null;
            }
        }
        if (hookBehavior == HookBehavior.RetractingWithPlayer)
        {
            Vector3 vel = (hookTr.position - player.position).normalized * playerRetractingSpeed;
            //playerRb.velocity = Vector3.MoveTowards(playerRb.velocity, vel, Time.deltaTime);
            if (Vector3.SqrMagnitude(grabbedToThis.position - hookGunPoint.position) < 5f)
            {
                hookBehavior = HookBehavior.Retracting;
                SoundsController.Instance.PlayHookLoop(true);
            }

            playerRb.velocity = vel;
        }

    }

    IEnumerator FindPointToGrab()
    {
        int curId = 0;
        while (true)
        {
            if (hookTargetExists)
            {
                float curSqrDist = Vector3.SqrMagnitude(player.position - currentHookablePoint.position);
                curDotAngle = DotAngle(currentHookablePoint, Mathf.Sqrt(curSqrDist));
                if (curDotAngle < minDotAngle)
                {
                    hookTargetExists = false;
                }
                else
                {
                    float dist = Mathf.Sqrt(curSqrDist);
                    bool planetInTheWay = Physics.Raycast(player.position, currentHookablePoint.position - player.position, dist, planetsOnly);
                    if (planetInTheWay) { hookTargetExists = false; }
                }
            }

            for (int i = 0; i < checksAmountPerIteration; i++)
            {
                curId++;
                curId %= hookableAsteroids.Count;
                float sqrDist = Vector3.SqrMagnitude(player.position - hookableAsteroids[curId].position);
                if (sqrDist < hookDistSqr)
                {
                    float dist = Mathf.Sqrt(sqrDist);
                    float dotAngle = DotAngle(hookableAsteroids[curId], dist);
                    if (hookTargetExists && dotAngle < curDotAngle)
                    {
                        //old target is better                        
                    }
                    else
                    {
                        if (dotAngle > minDotAngle)
                        {
                            bool planetInTheWay = Physics.Raycast(player.position, hookableAsteroids[curId].position - player.position, dist, planetsOnly);
                            if (!planetInTheWay)
                            {
                                curDotAngle = dotAngle;
                                hookTargetExists = true;
                                currentHookablePoint = hookableAsteroids[curId];
                            }
                        }
                    }
                }
            }

            if (hookTargetExists)
            {
                ShowCrosshair();
            }
            else
            {
                HideCrossHair();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    float DotAngle(Transform transf, float distance)
    {
        return Vector3.Dot(player.forward, (transf.position - player.position) / distance);
    }

    void ShowCrosshair()
    {
        crosshair.gameObject.SetActive(true);
    }
    void HideCrossHair()
    {
        crosshair.gameObject.SetActive(false);
    }

}
