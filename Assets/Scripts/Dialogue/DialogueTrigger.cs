using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public NavigationManager navigationManager;
    public DialogueManager dialogueManager;
    [SerializeField] private GameObject dialogueBox;
    public Dialogue dialogue;
    private bool inDialogue;
    
    // Start is called before the first frame update
    void Start()
    {

        TriggerDialogue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);

    }


}
