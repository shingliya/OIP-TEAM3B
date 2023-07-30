using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorButtonScript : MonoBehaviour
{
    public GameObject image; // Reference to the Image component
    public GameObject image2; // The first sprite

    // Call this function to toggle between the two sprites
    public void SwitchToFloor1()
    {
        image2.SetActive(false);
        image.SetActive(true);
    }

     public void SwitchToFloor2()
    {
        image.SetActive(false);
        image2.SetActive(true);
    }
}
