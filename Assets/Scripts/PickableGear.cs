using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableGear : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteraction()
    {
        QuestProgression.Instance.CollectCog();
        Destroy(gameObject);
    }
}
