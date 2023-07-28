using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WelcomeController : MonoBehaviour
{
    public TMP_InputField nameField;
    public GameObject emptyNameWarning;

    public void enterName()
    {
        if(nameField.text == "")
        {
            emptyNameWarning.SetActive(false);
            emptyNameWarning.SetActive(true);
        }
        else 
        {
            PlayerPrefs.SetString("name", nameField.text);
            PlayerPrefs.Save();

            print(PlayerPrefs.GetString("name", "friend"));

            GetComponent<ChangeLevelScript>().ChangeToScene();
        }
    }
}
