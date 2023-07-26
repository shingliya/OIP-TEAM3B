using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeScript : MonoBehaviour
{
    private Image image; // Reference to the Button component
    public Sprite newSprite; // The new sprite you want to assign to the Button

    private void Start()
    {
        // Get the Button component from the current GameObject
        image = GetComponent<Image>();

        // Check if a Button component is found on this GameObject
        if (image == null)
        {
            Debug.LogError("ChangeButtonSprite script requires a Image component attached to the same GameObject.");
        }
    }

    // Call this function to change the sprite of the Button
    public void ChangeSprite()
    {
        if (image != null && newSprite != null)
        {
            // Get the Image component from the Button
            Image buttonImage = image.GetComponent<Image>();
            if (buttonImage != null)
            {
                // Assign the new sprite to the Button's Image component
                buttonImage.sprite = newSprite;
            }
        }
    }
}
