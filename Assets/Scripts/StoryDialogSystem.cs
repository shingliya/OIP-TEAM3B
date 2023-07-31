using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro; // Import the namespace for TextMeshPro

public class StoryDialogSystem : MonoBehaviour
{
    [System.Serializable]
    public class DialogLine
    {
        public string text;
        public UnityEvent onDialogComplete; // Events to trigger on dialog completion.
    }  

    public TMP_Text dialogText;
    public Button nextButton;

    public DialogLine[] dialogLines;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private float typingSpeed = 0.05f;

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("name", "friend");
        foreach (DialogLine dialogLine in dialogLines)
        {
            if(dialogLine.text.Contains("[PlayerName]"))
                dialogLine.text = dialogLine.text.Replace("[PlayerName]", playerName);
        }

        nextButton.onClick.AddListener(NextButtonClick);
        ShowDialogLine();
    }

    private void ShowDialogLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeText(dialogLines[currentLineIndex].text));
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        bool comandMode = false;
        string comand = "";

        foreach (char letter in text)
        {
            if(letter == '<')
            {
                comandMode = true;
                comand += letter;
                continue;
            }
            if(comandMode)
            {
                comand += letter;
                if(letter == '>')
                {
                    comandMode = false;
                    dialogText.text += comand;
                    comand = "";
                }
            }
            else
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        typingCoroutine = null;
    }

    private void NextButtonClick()
    {
        if (typingCoroutine != null)
        {
            // Fast-forward the text
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            dialogText.text = dialogLines[currentLineIndex].text;
        }
        else
        {
            // Go to the next dialog line
            if (dialogLines[currentLineIndex].onDialogComplete.GetPersistentEventCount() != 0)
                dialogLines[currentLineIndex].onDialogComplete.Invoke();
            else
            {
                currentLineIndex++;
                ShowDialogLine();
            }
        }
    }

    public void ResumeDialog()
    {
        currentLineIndex++;
        ShowDialogLine();
    }
}
