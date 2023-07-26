using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorButtonScript : MonoBehaviour
{
    public Image image; // Reference to the Image component
    public Sprite sprite1; // The first sprite
    public Sprite sprite2; // The second sprite

    private void Start()
    {
        if (image == null)
        {
            // Get the Image component from the current GameObject if not assigned
            image = GetComponent<Image>();
        }

        if (image == null)
        {
            Debug.LogError("ToggleImage script requires an Image component attached to the same GameObject.");
        }
    }

    // Call this function to toggle between the two sprites
    public void SwitchToFloor1()
    {
        if (image != null && sprite1 != null && sprite2 != null)
        {
            image.sprite = sprite1;
        }
    }

     public void SwitchToFloor2()
    {
        if (image != null && sprite1 != null && sprite2 != null)
        {
            image.sprite = sprite2;
        }
    }
}
