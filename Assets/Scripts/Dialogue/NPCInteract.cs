using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public DialogueScene dialogueScene;  // Reference to the ScriptableObject
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    private void OnMouseDown()
    {
       dialogueManager.StartDialogue(dialogueScene);
    }
}
