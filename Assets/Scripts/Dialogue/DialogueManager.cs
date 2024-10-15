using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text name;
    public TMP_Text sentence;

    private Queue<DialogueLine> lines;
    public bool isDialogueActive = false;
    public float typingSpeed = 0.5f;
    public Animator animator;
    private Coroutine typingCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        lines = new Queue<DialogueLine>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        animator.SetBool("FadeIn", true);

        lines.Clear();

        foreach(DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if(lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        Debug.Log("Current Line: " + currentLine.sentences);
        name.text = currentLine.name;
        StopTypingCoroutine();
        typingCoroutine = StartCoroutine(TypeSentence(currentLine));
    }

    private void StopTypingCoroutine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        sentence.text = "";
        foreach(char letter in dialogueLine.sentences.ToCharArray())
        {
            sentence.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("FadeIn", false);
        if(NPCInteract.Instance.ShouldStartInterrogation() || NPCInteract.Instance.IsInInterrogation())
        {
            NPCInteract.Instance.StartInterrogation();
            NPCInteract.Instance.shouldStartInterrogation = false; // Reset flag
        }
    }

    private void SkipDialogue()
    {
        // Check if typing coroutine is active
        StopAllCoroutines(); // Stop current typing
        if (lines.Count > 0)
        {
            DialogueLine currentLine = lines.Peek(); // Peek at the current line
            sentence.text = currentLine.sentences; // Show full sentence
            DisplayNextDialogue(); // Move to next dialogue
        }
    }   

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}
