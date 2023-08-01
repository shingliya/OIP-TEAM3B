using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
     private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            // Make the GameObject persistent across scenes
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // If another instance of the GameObject already exists, destroy this one
            Destroy(this.gameObject);
        }
    }
}
