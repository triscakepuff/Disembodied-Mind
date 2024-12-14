using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNPCs : MonoBehaviour
{
    public Dialogue NPCDialogue;
    public DialogueManager dialogueManager;
    // Update is called once per frame
    private void OnMouseDown()
    {
        dialogueManager.StartDialogue(NPCDialogue);
    }
}
