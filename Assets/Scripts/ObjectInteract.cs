using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObjectInteract : MonoBehaviour
{   
    public string[] ObjectName;
    public string targetObject;
    public TMP_Text dialogueLine;
    public string interactLine;
    public string interactLine2;
    public int dialogueTime;
    private Animator dialogueAnimator;
    private SpriteRenderer spriteRenderer;

    public QuestManager questManager;
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

            if(questManager.quests[0].questName == "Gather items for the ritual")
            {
                if((SelectedItem.itemName == ObjectName[0] || SelectedItem.itemName == ObjectName[1]))
                {
                    StartCoroutine(InstantiateDialogue());
                    dialogueAnimator.SetBool("FadeIn", true);
                    dialogueLine.text = interactLine2;
                    if(SelectedItem.itemName == "Empty Oil Can" || SelectedItem.itemName == "Empty Oil Can 1" )
                    {
                        Sprite filledOilCanSprite = GameManager.Instance.filledOilCan.sprite;
                        questManager.CompleteTask(0);   
                        Inventory.instance.ModifyItem(SelectedItem, "Filled Oil Can", filledOilCanSprite);  
                    }
                }
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

