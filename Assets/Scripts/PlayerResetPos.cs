using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetPos : MonoBehaviour
{
    Vector3 initialPos;
    float tooFar = 200;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        StartCoroutine(CheckDistance());
    }


    IEnumerator CheckDistance()
    {
        while (true)
        {
            if (Vector3.Distance(initialPos, transform.position) > tooFar)
            {
                transform.position = initialPos;
                //GetComponent<Rigidbody>().velocity = (initialPos - transform.position).normalized * 20f;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            yield return new WaitForSeconds(1);
        }
    }

}
