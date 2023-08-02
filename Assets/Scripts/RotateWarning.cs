using UnityEngine;
using UnityEngine.UI;

public class RotateWarning : MonoBehaviour
{
    Canvas c;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        c = gameObject.GetComponent<Canvas>();
    }
    void Update()
    {
        if (Screen.height > Screen.width)
        {
            c.enabled = true;
            Time.timeScale = 0f;
        }
        else
        {
            c.enabled = false;
            Time.timeScale = 1f;
        }

    }

}

