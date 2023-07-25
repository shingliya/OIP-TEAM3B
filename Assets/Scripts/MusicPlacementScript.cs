using UnityEngine;

public class MusicPlacementScript : MonoBehaviour
{

    public MusicVaseScript currentVase = null; 
    private MeshRenderer meshRenderer; 
    
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component
        meshRenderer.enabled = false; // Set the initial state to disabled
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("vase"))
        {
            MusicVaseScript temp = other.GetComponent<MusicVaseScript>();

            if(currentVase == null)
            {
                currentVase = temp;
            }
            if(temp.isDragging && temp.gameObject != currentVase.gameObject)
            {
                meshRenderer.enabled = true; // Enable the MeshRenderer when entering the snapping range
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("vase"))
        {
            if(currentVase.gameObject == other.gameObject)
            {
                currentVase = null;
            }
            
            meshRenderer.enabled = false; // Enable the MeshRenderer when entering the snapping range
            
        }
    }
}
