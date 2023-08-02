using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentAudio : MonoBehaviour
{
    private static bool created = false;
    private AudioSource audioSource;

    void Awake()
    {
        if (!created)
        {
            // Make the GameObject persistent across scenes
            DontDestroyOnLoad(this.gameObject);
            created = true;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            // If another instance of the GameObject already exists, destroy this one
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check if the current scene's name is the specified scene name
        if (currentScene.name == "MusicVaseScene")
        {
            // Lower the volume in the specified scene
            audioSource.volume = 0.5f;
        }
        else
        {
            // Set the volume to the normal volume in other scenes
            audioSource.volume = 1;
        }
    }
}
