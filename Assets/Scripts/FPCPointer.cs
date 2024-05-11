using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCPointer : MonoBehaviour
{
    public LayerMask interactables;
    Transform camTr;

    public GameObject canUseIndicator;
    void Start()
    {
        camTr = Camera.main.transform;
    }

    void Update()
    {
        Ray ray = new Ray(camTr.position, camTr.forward);
        RaycastHit hit;
        bool canPressE = Physics.Raycast(ray, out hit, 5f, interactables);

        if (canPressE) { canPressE = Vector3.SqrMagnitude(camTr.position - hit.point) < 5f; }

        canUseIndicator.SetActive(canPressE);

        if (canPressE && Input.GetKeyDown(KeyCode.E))
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