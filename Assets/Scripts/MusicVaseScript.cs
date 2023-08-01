using UnityEngine;

public class MusicVaseScript : MonoBehaviour
{
    public AudioClip clickSound;

    public bool isDragging = false;
    public MusicPlacementScript currentPlacement = null;
    public MusicPlacementScript targetPlacement;

    private Vector3 offset;
    private const float clickThreshold = 0.15f; // Adjust this value based on your needs.

    private float zOffset;
    
    private AudioSource audioSource;
    private ParticleSystem particleSys;
    public AudioClip moveSFX;
    public AudioSource sfxSource;

    void Start()
    {
        zOffset = transform.position.z;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        particleSys = GetComponent<ParticleSystem>();
        particleSys.Stop();
    }

    private void OnMouseDown()
    {
        // Calculate the offset for more accurate dragging.
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        isDragging = false; // Initialize as not dragging until proven otherwise.
        targetPlacement = null;
    }

    private void OnMouseDrag()
    {
        if (!isDragging)
        {
            // Check if the drag distance exceeds the threshold.
            Vector3 dragDelta = (Vector2)currentPlacement.transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (dragDelta.magnitude >= clickThreshold)
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            // Update the object's position while dragging.
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, currentPlacement.transform.position.z - 1);
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!isDragging)
        {
            // If the click was detected without dragging, perform click behavior.
            if (clickSound != null)
            {
                audioSource.PlayOneShot(clickSound);
                particleSys.Play();
            }
            Debug.Log("Object clicked!");
            // Implement any click behavior you want here.
        }
        else
        {
            // If a target object is assigned, swap positions with it.
            if (targetPlacement != null)
            {
                MusicVaseScript otherVase = targetPlacement.currentVase;
                otherVase.transform.position = new Vector3(currentPlacement.transform.position.x, currentPlacement.transform.position.y, zOffset);
                otherVase.currentPlacement = currentPlacement;
                currentPlacement.currentVase = otherVase;

                transform.position = new Vector3(targetPlacement.transform.position.x, targetPlacement.transform.position.y, zOffset);
                currentPlacement = targetPlacement;
                currentPlacement.currentVase = this;

                sfxSource.PlayOneShot(moveSFX);
            }
            else
            {
                // If no target object is found, return the object to its initial position.
                transform.position = new Vector3(currentPlacement.transform.position.x, currentPlacement.transform.position.y, zOffset);
            }
        }

        isDragging = false;
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snapPoint"))
        {
            targetPlacement = other.GetComponent<MusicPlacementScript>();

            if(currentPlacement == null)
            {
                currentPlacement = targetPlacement;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("snapPoint"))
        {
            targetPlacement = null;
        }
    }
}