using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Scene", menuName = "Dialogue/Dialogue Scene")]
public class DialogueScene : ScriptableObject
{
    public DialogueLine[] dialogueLines; // Array of dialogue lines for this scene
}
