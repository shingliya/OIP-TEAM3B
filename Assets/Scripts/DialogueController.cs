using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public TMP_Text dialogueText; // Reference to the Text component to display dialogue text
    public GameObject dialogueBox; // Reference to the GameObject that contains the dialogue UI
    public float typingSpeed = 0.05f; // Speed at which the text is displayed (typewriter effect)
    public AudioSource audioSource;
    public AudioClip talkSFX;

    private List<string> dialogueLines = new List<string>
    {
        "Hello there, <b>[PlayerName]</b>!\nWelcome to the Hunterian Museum.",
        "My name is <b>Ms Monocle</b> because I just love monocles!",
        "Marvellous discoveries and adventures lie ahead, let’s GO!"
    };
    private int currentLineIndex = 0; // Index of the current dialogue line
    private bool isTyping = false; // Flag to check if text is currently being displayed


    private void Start()
    {
        string playerName = PlayerPrefs.GetString("name", "friend");
        if (!string.IsNullOrEmpty(playerName))
        {
            dialogueLines[0] = dialogueLines[0].Replace("[PlayerName]", playerName);
        }
        // Hide the dialogue box at the beginning
        dialogueBox.SetActive(false);
        StartConversation(dialogueLines);
    }

    // Method to start the conversation with a list of dialogue lines
    public void StartConversation(List<string> lines)
    {
        dialogueBox.SetActive(true);
        dialogueLines = lines;
        currentLineIndex = 0;
        ShowNextLine();
    }

    // Method to display the next dialogue line
    public void ShowNextLine()
    {
        if (!isTyping){
            // Check if all dialogue lines have been shown
            if (currentLineIndex >= dialogueLines.Count)
            {
                EndConversation();
                return;
            }

            // Display the dialogue line with a typewriter effect
            StartCoroutine(TypeDialogue(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
    }

    private bool isFastForwarding = false; // Flag to indicate if fast forward is active

    private IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        bool comandMode = false;
        string comand = "";

        foreach (char letter in line.ToCharArray())
        {
            if (isFastForwarding)
            {
                dialogueText.text = line;
                isFastForwarding = false;
                isTyping = false;
                break;
            }

            if (letter != ' ')
            {
                audioSource.PlayOneShot(talkSFX);
            }

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
                    dialogueText.text += comand;
                    comand = "";
                }
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        isTyping = false;
    }

    // Method to activate fast forward
    public void FastForwardDialogue()
    {
        if (isTyping){
            isFastForwarding = true;
        }
    }

    // Method to end the conversation and hide the dialogue box
    private void EndConversation()
    {
        dialogueBox.SetActive(false);
    }
}
