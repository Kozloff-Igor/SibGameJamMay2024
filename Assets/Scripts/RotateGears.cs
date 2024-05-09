using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGears : MonoBehaviour
{
    [SerializeField] private List<Gears> groupGears;

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<Gears>() != null)
            {
                groupGears.Add(this.transform.GetChild(i).GetComponent<Gears>());

            }

        }

        if (groupGears.Count == 10)
        {
            for (int i = 0; i < groupGears.Count; i++)
            {
                groupGears[i].FixedUpdate();
            }
        }
    }
}
