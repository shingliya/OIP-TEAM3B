using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildComponentScript : MonoBehaviour
{
    PuzzleScript parentScript;
    MeshRenderer meshRenderer; 
    
    public ChildComponentScript currentLink;

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

    void Start()
    {
        Transform rootParent = GetRootParent(transform);
        parentScript = rootParent.GetComponent<PuzzleScript>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        currentLink = null;
    }

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
                //meshRenderer.enabled = false; // Disable the MeshRenderer when exiting the snapping range
                currentLink = null;
                parentScript.OnChildTriggerExit(this, other.gameObject.transform);
            }
        }
    }
}

