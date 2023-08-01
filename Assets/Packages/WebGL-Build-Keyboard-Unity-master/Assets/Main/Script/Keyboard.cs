using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    
    public TMP_InputField inputField;
    public TMP_InputField displayField;
    public Button uppercaseButton;
    
    bool uppercasePressed = false;

    private float minKByPos = -172;
    private float maxKByPos = 130;
    RectTransform rectTransform;
    private Coroutine coroutine = null;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.y = minKByPos;
        rectTransform.anchoredPosition = anchoredPosition;
    }  

    public void PopUp()
    {
        if (coroutine != null)
        {
            return;
        }
        coroutine = StartCoroutine(AnimateKeyboard(minKByPos, maxKByPos));
        displayField.text = inputField.text;
        displayField.ActivateInputField();
        displayField.caretPosition = displayField.text.Length;
    }

    public void PopDown()
    {
        if (coroutine != null)
        {
            return;
        }
        coroutine = StartCoroutine(AnimateKeyboard(maxKByPos, minKByPos));
        displayField.text = "";
        displayField.DeactivateInputField();
    }
    
    private IEnumerator AnimateKeyboard(float startY, float endY)
    {
        float timer = 0f;
        float duration = 0.5f;

        while (timer <  duration)
        {
            float progress = timer / duration;

            Vector2 anchoredPosition = rectTransform.anchoredPosition;
            anchoredPosition.y = Mathf.Lerp(startY, endY, progress);
            rectTransform.anchoredPosition = anchoredPosition;

            timer += Time.deltaTime;

            yield return null;
        }

        coroutine = null;
    }

    public void onKeyboardButtonClick()
    {
        string buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        
        if (buttonPressed.Equals("UPPER"))
        {
            if (uppercasePressed)
            {
                uppercaseButton.GetComponent<Image>().color = Color.white;
                uppercasePressed = false;
            }
            else
            {
                uppercaseButton.GetComponent<Image>().color = Color.gray;
                uppercasePressed = true;
            }
        }
        else
        {
            if (uppercasePressed)
            {
                buttonPressed = buttonPressed.ToUpper();
            }
            else
            {
                buttonPressed = buttonPressed.ToLower();
            }

            int cursorPosition = displayField.caretPosition;
            if (cursorPosition >= 0)
            {
                string currentText = displayField.text;
                if (buttonPressed.Equals("CANC") || buttonPressed.Equals("canc"))
                {
                    if (cursorPosition > 0 && cursorPosition <= currentText.Length)
                    {
                        currentText = currentText.Remove(cursorPosition - 1, 1);

                        displayField.text = inputField.text = currentText;
                        displayField.caretPosition = cursorPosition - 1;
                        displayField.ActivateInputField();
                    }
                }
                else
                {
                    if (buttonPressed.Equals("ENTER") || buttonPressed.Equals("enter") || buttonPressed.Equals("EMPTY") || buttonPressed.Equals("empty") || buttonPressed.Equals("UPPER")||buttonPressed.Equals("upper"))
                    {
                    }
                    else
                    {
                        currentText = currentText.Insert(cursorPosition, buttonPressed);

                        displayField.text = inputField.text = currentText;
                        displayField.caretPosition = cursorPosition + 1;
                        displayField.ActivateInputField();
                    }
                }

            }
        }
    }


}
