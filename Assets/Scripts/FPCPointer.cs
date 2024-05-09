using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCPointer : MonoBehaviour
{
    public LayerMask interactables;
    Transform camTr;
    void Start()
    {
        camTr = Camera.main.transform;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(camTr.position, camTr.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 5f, interactables))
            {
                IInteractable interactable;
                bool isInteractable = hit.transform.TryGetComponent<IInteractable>(out interactable);
                if (isInteractable) 
                {
                    interactable.OnInteraction();
                }
            }
        }
    }
}