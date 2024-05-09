using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IInteractable
{

    public void OnInteraction()
    {
        Debug.Log("Player pressed E on me", gameObject);

    }
}
