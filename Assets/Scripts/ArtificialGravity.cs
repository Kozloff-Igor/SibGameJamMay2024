using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class ArtificialGravity : MonoBehaviour
{
    public enum StickMode { DownToPivot, DownToNormal }
    public StickMode stickMode;
    public bool magneticBoots;

    Transform[] asteroidsWithGravity;
    Transform player;
    Rigidbody playerRb;
    public Transform closestAsteroid;
    public LayerMask gravityOnly;
    float sqrDistToGravitySource;
    public AnimationCurve gravityOverDistance;
    public float grav = 9.8f;

    bool currentlyHooking;

    void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("HaveGravity");
        asteroidsWithGravity = new Transform[g.Length];
        for (int q = 0; q < g.Length; q++)
        {
            asteroidsWithGravity[q] = g[q].transform;
        }

        player = FindObjectOfType<FirstPersonController>().transform;
        playerRb = player.GetComponent<Rigidbody>();
        closestAsteroid = ClosestAsteroidWithGravity();
    }

    void FixedUpdate()
    {
        closestAsteroid = ClosestAsteroidWithGravity();
        Vector3 dir = (closestAsteroid.position - (player.position)).normalized;
        //Ray ray = new Ray(player.position - player.up * 0.7f, dir);
        Ray ray = new Ray(player.position, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, gravityOnly))
        {
            if (stickMode == StickMode.DownToPivot)
            {
                transform.position = hit.point - dir;
                //transform.up = -dir;
            }
            else
            {
                transform.position = hit.point + hit.normal;
                //transform.up = hit.normal;
            }

            float dist = Vector3.Distance(hit.point, player.position);
            float gravForce = gravityOverDistance.Evaluate(dist) * grav;
            if (magneticBoots)
            {
                playerRb.MovePosition(transform.position);
            }
            else
            {
                if (Vector3.SqrMagnitude(transform.position - playerRb.position) > 0.01f) //no sliding on cube planets
                {
                    playerRb.AddForce(dir * gravForce, ForceMode.Force);
                }
            }
        }
        //Quaternion lookRot = Quaternion.LookRotation(player.forward, transform.up);
        Vector3 projForwNoNorm = (player.forward - Vector3.Project(player.forward, -dir)).normalized;

        Quaternion lookRot = Quaternion.LookRotation(player.forward, -dir);
        lookRot = Quaternion.LookRotation(projForwNoNorm, -dir);
        if (stickMode == StickMode.DownToNormal)
        {
            //lookRot = Quaternion.LookRotation(player.forward, hit.normal);
            projForwNoNorm = (player.forward - Vector3.Project(player.forward, hit.normal)).normalized;
            lookRot = Quaternion.LookRotation(projForwNoNorm, hit.normal);
            //    lookRot = Quaternion.LookRotation(projForwNoNorm, -dir);
        }
        transform.rotation = lookRot;
        float rotSpeedModifier = 1 / (1 + Vector3.Distance(playerRb.position, transform.position));
        //Quaternion playerTargetRot = Quaternion.RotateTowards(playerRb.rotation, transform.rotation, 190f * Time.deltaTime);
        Quaternion playerTargetRot = Quaternion.RotateTowards(playerRb.rotation, transform.rotation, 290f * Time.deltaTime * rotSpeedModifier);
        playerRb.MoveRotation(playerTargetRot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && false)//nah, no magnetic for now
        {
            magneticBoots = !magneticBoots;
            playerRb.isKinematic = magneticBoots;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Break();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (stickMode == StickMode.DownToPivot)
            {
                stickMode = StickMode.DownToNormal;
                return;
            }
            if (stickMode == StickMode.DownToNormal)
            {
                stickMode = StickMode.DownToPivot;
                return;
            }
        }

    }

    Transform ClosestAsteroidWithGravity()
    {
        Transform t = asteroidsWithGravity[0];
        float minDist = Vector3.SqrMagnitude(t.position - player.position);
        for (int q = 0; q < asteroidsWithGravity.Length; q++)
        {
            float dist = Vector3.SqrMagnitude(asteroidsWithGravity[q].position - player.position);
            if (dist < minDist)
            {
                minDist = dist;
                t = asteroidsWithGravity[q];
            }
        }

        return t;
    }
}
