using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public bool isSnapped = false;
    private Transform[] snapPoints;
    public Transform currentSnapPoint;
    public Transform targetSnapPoint;
    private Vector3 offset;
    private bool isDragging = false;
    public bool isMouseUp = true;

    private Renderer meshRenderer;

    public PuzzleGameController gameController;

    void Start()
    {
        // Get the Mesh Renderer component attached to this GameObject
        meshRenderer = GetComponent<Renderer>();

        gameController = FindObjectOfType<PuzzleGameController>();
        snapPoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            snapPoints[i] = transform.GetChild(i);
        }
    }

    void OnMouseDown()
    {
        isSnapped = false;
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        offset = transform.position - touchPos;
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (!isSnapped && isDragging)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 newPosition = touchPos + offset;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        isMouseUp = true; // Set the flag to indicate that the mouse button is up
        // Perform raycast to check for obstacles between the snap points
        if (currentSnapPoint != null && targetSnapPoint != null)
        {
            Vector3 direction = targetSnapPoint.position - currentSnapPoint.position;
            float distance = direction.magnitude;

            Ray ray = new Ray(currentSnapPoint.position, direction.normalized);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance);

             // Draw the raycast in the Scene view to visualize it
            Debug.DrawRay(currentSnapPoint.position, direction.normalized * distance, Color.blue, 2f);

/*
            // Check for any colliders hit by the raycast
            foreach (RaycastHit hit in hits)
            {
                print(hit.transform.gameObject);
                if (hit.collider != null && hit.collider != currentSnapPoint && hit.collider != targetSnapPoint)
                {
                    currentSnapPoint = null;
                    targetSnapPoint = null;
                    return; // Exit the method, no snapping should occur
                }
            }
*/
            // Snap the pieces together if there are no obstacles between the snap points
            Vector3 translation = targetSnapPoint.position - currentSnapPoint.position;
            isSnapped = true;
            transform.Translate(translation);
            if (gameController != null)
            {
                print("check game over");
                gameController.ValidateMove();
            }
        }
    }

    public void OnChildTriggerEnter(ChildComponentScript childCollider, Transform other)
    {
        //if(!isSnapped)
         {
        print("in");
        currentSnapPoint = childCollider.gameObject.transform;
        targetSnapPoint = other; 
        }

        if (meshRenderer.material.shader.name.Contains("Standard"))
        {
            // Set the new metallic value for the material
            meshRenderer.material.SetFloat("_Metallic", 0f);
        }
    }

    public void OnChildTriggerExit(ChildComponentScript childCollider, Transform other)
    {
        //if(!isSnapped) 
        {
        print("out");
        currentSnapPoint = null;
        targetSnapPoint = null;

        }

        ChildComponentScript[] childComponents = GetComponentsInChildren<ChildComponentScript>();
        
        foreach (ChildComponentScript childComponent in childComponents)
        {
            if(childComponent.currentLink != null)
            {
                return;
            }
        }
        
        if (meshRenderer.material.shader.name.Contains("Standard"))
        {
            // Set the new metallic value for the material
            meshRenderer.material.SetFloat("_Metallic", 0.4f);
        }
    }
}
