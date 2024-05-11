using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPin : MonoBehaviour
{
    [System.Serializable]
    public struct Neighbour
    {
        public GearPin gearPin;
        public GearSingle.Size neighbourSize;
        public GearSingle.Size mySize;
    }
    public Neighbour[] CanSpinCryteria;
    public Neighbour[] BlockedCryteria;

    public GearSingle myCurrentGear;
    public bool isClockwise;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckSpinning();
    }

    void CheckSpinning()
    {
        if (CanSpinCryteria.Length == 0) return;
        if (myCurrentGear == null) return;
        GearSingle poweredNeighbor = CanSpinCryteria[0].gearPin.myCurrentGear;
        if (poweredNeighbor != null)
        {
            myCurrentGear.curentlyRotating = poweredNeighbor.curentlyRotating;
        } else{
            myCurrentGear.curentlyRotating = false;
        }
    }

    public bool CheckIfICanBePlaced(GearSingle gearSingle)
    {
        if (myCurrentGear != null) return false;
        GearSingle.Size size = gearSingle.size;
        for (int q = 0; q < BlockedCryteria.Length; q++)
        {
            if (BlockedCryteria[q].mySize == size)
            {
                GearSingle neighbourGear = BlockedCryteria[q].gearPin.myCurrentGear;
                if (neighbourGear != null)
                {
                    if (neighbourGear.size == BlockedCryteria[q].neighbourSize)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    bool IsBlocked(Neighbour n, GearSingle.Size size)
    {
               

        return true;
    }
}
