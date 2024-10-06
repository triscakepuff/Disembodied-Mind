using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public TMP_Text speakerText;    // Reference to the speaker name UI Text
    public TMP_Text dialogueText;   // Reference to the dialogue UI Text

    //public Image portraitImage; // Reference to the character portrait UI Image
    public GameObject dialoguePanel; // Reference to the dialogue UI Panel
    public float typingSpeed = 0.05f; // Time between each letter
    public Animator dialogueAnimator;
    private DialogueLine currentDialogue;
    private Coroutine typingCoroutine;
    private int dialogueIndex;
    private bool isTyping = false; // To keep track of typing state

    void Start()
    {
        dialoguePanel.SetActive(false); // Hide the dialogue panel at the start
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(isTyping)
            {
                SkipTyping();
            }
        }
    }
    public void StartDialogue(DialogueScene dialogueScene)
    {
        dialoguePanel.SetActive(true); // Show the dialogue panel
        dialogueIndex = 0;
        DisplayDialogue(dialogueScene.dialogueLines[dialogueIndex]); 
    }

    void DisplayDialogue(DialogueLine dialogueLine)
    {
        
        currentDialogue = dialogueLine;
        speakerText.text = dialogueLine.name;

        // Show character portrait
        // if (dialogueLine.characterPortrait != null)
        // {
        //     portraitImage.sprite = dialogueLine.characterPortrait;
        //     portraitImage.gameObject.SetActive(true);
        // }
        // else
        // {
        //     portraitImage.gameObject.SetActive(false);
        // }

        // Start typing effect
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeSentence(dialogueLine.dialogue));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Clear the dialogue text
        isTyping = true;        // Mark that we are typing

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;  // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed); // Wait before next letter
        }

        isTyping = false;  // Typing finished
        // Optionally, enable choice buttons here if needed
    }

    void StartTyping(DialogueLine dialogueLine)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        currentDialogue = dialogueLine;
        typingCoroutine = StartCoroutine(TypeSentence(dialogueLine.dialogue));
    }
    public void NextDialogue(DialogueScene dialogueScene)
    {
        if (dialogueIndex < dialogueScene.dialogueLines.Length - 1)
        {
            dialogueIndex++;
            DisplayDialogue(dialogueScene.dialogueLines[dialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }
    // Optionally allow player to skip the typing effect
    public void SkipTyping()
    {
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.dialogue;  // Show the full text
            isTyping = false;
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Hide the dialogue panel when done
    }
}
