using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the namespace for TextMeshPro

public class LeadersboardController : MonoBehaviour
{
    public TMP_Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        string playerName = PlayerPrefs.GetString("name", "friend");
        nameText.text = playerName;
    }
}
