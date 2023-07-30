using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildComponentScript : MonoBehaviour
{
    PuzzleScript parentScript;
    MeshRenderer meshRenderer; // Reference to the MeshRenderer component
    
    public ChildComponentScript currentLink; // The other snappoint that this snappoint is currently linked to

    private Transform GetRootParent(Transform childTransform)
    {
        Transform parentTransform = childTransform.parent;
        while (parentTransform != null)
        {
            childTransform = parentTransform;
            parentTransform = parentTransform.parent;
        }
        return childTransform;
    }

    // ... (other code remains unchanged)

    void Start()
    {
        Transform rootParent = GetRootParent(transform);
        parentScript = rootParent.GetComponent<PuzzleScript>();
        meshRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component
        meshRenderer.enabled = false; // Set the initial state to disabled
        currentLink = null;
    }

    // ... (other code remains unchanged)

    // Implement the OnTriggerEnter method for the child component
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snapPoint"))
        {
            ChildComponentScript temp = other.GetComponent<ChildComponentScript>();
            if(currentLink == null || currentLink == temp)
            {
                parentScript.OnChildTriggerEnter(this, other.gameObject.transform);
                //meshRenderer.enabled = true; // Enable the MeshRenderer when entering the snapping range
                currentLink = temp;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("snapPoint"))
        {
            ChildComponentScript temp = other.GetComponent<ChildComponentScript>();
            if(currentLink == null || currentLink == temp)
            {
                parentScript.OnChildTriggerExit(this, other.gameObject.transform);
                //meshRenderer.enabled = false; // Disable the MeshRenderer when exiting the snapping range
                currentLink = null;
            }
        }
    }
}

