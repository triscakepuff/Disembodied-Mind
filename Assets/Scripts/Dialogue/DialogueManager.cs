using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; // UI for the speaker's name
    public TextMeshProUGUI dialogueText; // UI for the dialogue text
    public GameObject dialogueBox; // Dialogue UI box
    private Queue<DialogueLine> dialogueQueue; // Queue for dialogue lines
    public string currSentence;
    private Animator dialogueAnim;
    public bool inDialogue = false;

    public static DialogueManager instance;

    void Start()
    {
        dialogueQueue = new Queue<DialogueLine>();
        dialogueAnim = dialogueBox.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            CompleteSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        dialogueAnim.SetBool("FadeIn", true);

        dialogueQueue.Clear();
        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        currSentence = currentLine.sentences;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine.name, currentLine.sentences));
    }

    IEnumerator TypeSentence(string speakerName, string sentence)
    {
        nameText.text = speakerName;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void CompleteSentence()
    {
        if (dialogueText.text != currSentence)
        {
            StopAllCoroutines(); // Stop the typing coroutine.
            dialogueText.text = currSentence;
        }
        else
        {
            
            DisplayNextLine();
        }
    }

    void EndDialogue()
    {
        inDialogue = false;
        dialogueAnim.SetBool("FadeIn", false);
        Debug.Log("Dialogue ended.");
    }

    public bool InDialogue()
    {
        return inDialogue;
    }
}
