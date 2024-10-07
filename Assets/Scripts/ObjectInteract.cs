using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectInteract : MonoBehaviour
{   
    public string ObjectName;
    public string targetObject;
    public TMP_Text dialogueLine;
    public string interactLine;
    public string interactLine2;
    public int dialogueTime;
    private Animator dialogueAnimator;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        if(dialogueLine != null)
        {
            dialogueAnimator = dialogueLine.GetComponent<Animator>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(Inventory.instance.IsItemSelected())
        {
            Item SelectedItem = Inventory.instance.GetSelectedItem();

            if(SelectedItem.itemName == ObjectName && gameObject.name == targetObject)
            {
                Debug.Log("I finally did something about this object!");
                StartCoroutine(InstantiateDialogue());
                dialogueAnimator.SetBool("FadeIn", true);
                dialogueLine.text = interactLine2;
                Inventory.instance.RemoveItem(SelectedItem);
                spriteRenderer.color = Color.red;
            }
            
        }else
        {
            StartCoroutine(InstantiateDialogue());
            dialogueAnimator.SetBool("FadeIn", true);
            dialogueLine.text = interactLine;
        }
        
    }

    public IEnumerator InstantiateDialogue()
    {

        yield return new WaitForSeconds(dialogueTime);
        
        dialogueAnimator.SetBool("FadeIn", false);
        
    }
}

