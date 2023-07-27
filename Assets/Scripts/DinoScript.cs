using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoScript : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 touchPosition;
    private Vector3 targetPosition;
    private Vector3 offset;
    public float smoothingSpeed = 5f;

    private void OnMouseDown()
    {
        Vector3 screenInputPosition = Input.mousePosition;
        touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenInputPosition.x, screenInputPosition.y, Camera.main.nearClipPlane));
        touchPosition.y = transform.position.y; // Keep the Y position unchanged for top-down view
        if (GetComponent<Collider>().bounds.Contains(touchPosition))
        {
            isDragging = true;
            offset = transform.position - touchPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        Vector3 screenInputPosition = Input.mousePosition;
        touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenInputPosition.x, screenInputPosition.y, Camera.main.nearClipPlane));
        touchPosition.y = transform.position.y; // Keep the Y position unchanged for top-down view

        if (isDragging)
        {
            targetPosition = touchPosition + offset;
        }
    }

    private void Update()
    {
        // Apply smoothing to move the object smoothly
        if (isDragging)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothingSpeed);
            newPosition.y = transform.position.y; // Keep the Y position unchanged for top-down view
            transform.position = newPosition;

            Vector3 lookDir = touchPosition - transform.position;
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(90f, -angle, 0f); // Use Euler angles to maintain look-at behavior on the XZ plane
        }
    }
}
