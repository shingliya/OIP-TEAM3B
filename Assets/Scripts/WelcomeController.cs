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

    private CanvasGroup cur = null;
    
    public void setCur(CanvasGroup a)
    {
        cur = a;
    }

    public void nextPage(CanvasGroup next)
    {
        StartCoroutine(FadeInOut(cur, next));
    }

    private IEnumerator FadeInOut(CanvasGroup cur, CanvasGroup next)
    {
        float timer = 0f;
        float duration = 0.6f;

        cur.interactable = false;
        
        while (timer <  duration)
        {
            float progress = timer / duration;

            cur.alpha = Mathf.Lerp(1, 0, progress);

            timer += Time.deltaTime;

            yield return null;
        }

        cur.alpha = 0;
        cur.gameObject.SetActive(false);



        timer = 0f;

        next.gameObject.SetActive(true);

        while (timer <  duration)
        {
            float progress = timer / duration;

            next.alpha = Mathf.Lerp(0, 1, progress);

            timer += Time.deltaTime;

            yield return null;
        }

        next.alpha = 1;
        next.interactable = true;
    }
}
