using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelScript : MonoBehaviour
{
    public string sceneName; // The name of the scene you want to load

    // Call this function to change to another scene
    public void ChangeToScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not provided. Please assign a valid scene name.");
        }
    }
}
