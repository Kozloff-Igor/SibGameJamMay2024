using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
   public Transform gun;
   public Transform hook;
    
    void Update()
    {
        transform.position = (gun.position + hook.position) * 0.5f;
        transform.LookAt(hook);
        transform.localScale = new Vector3(1, 1, (gun.position - hook.position).magnitude);
        
    }
}
