using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectInteract : MonoBehaviour
{
    public TMP_Text dialogueLine;
    public string interactLine;
    public int dialogueTime;
    private Animator dialogueAnimator;
    void Start()
    {
        if(dialogueLine != null)
        {
            dialogueAnimator = dialogueLine.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("This is object scary!");
        StartCoroutine(InstantiateDialogue());
        dialogueAnimator.SetBool("FadeIn", true);
        dialogueLine.text = interactLine;
    }

    IEnumerator InstantiateDialogue()
    {

        yield return new WaitForSeconds(dialogueTime);
        
        dialogueAnimator.SetBool("FadeIn", false);
        
    }
}

