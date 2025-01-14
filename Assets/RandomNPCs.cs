using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNPCs : MonoBehaviour
{
    public Dialogue NPCDialogueDay4;
    public Dialogue NPCDialogueDay5;
    public Dialogue NPCDialogue;
    public DialogueManager dialogueManager;
    // Update is called once per frame
    private void OnMouseDown()
    {
        if(GameManager.Instance.currentDay == 5)
        {
            dialogueManager.StartDialogue(NPCDialogueDay5);
        }else if(GameManager.Instance.currentDay == 4)
        {
            dialogueManager.StartDialogue(NPCDialogueDay4);
        }
        else
        {
            dialogueManager.StartDialogue(NPCDialogue);
        }
        
    }
}
