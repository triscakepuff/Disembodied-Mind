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
    private bool hasTakenOil = false;

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
            Debug.Log("" + SelectedItem.itemName);

            StartCoroutine(InstantiateDialogue());
            dialogueAnimator.SetBool("FadeIn", true);
            dialogueLine.text = interactLine2;
            if((SelectedItem.itemName == "Empty Oil Can" || SelectedItem.itemName == "Empty Oil Can 1") && gameObject.name == "Oil Cans")
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.RemoveItem(SelectedItem, 1);
                Inventory.instance.AddItem(GameManager.Instance.filledOilCan);
                questManager.CompleteTask(0);   
            }
            if(SelectedItem.itemName == "Hairpin" && gameObject.name == "Daughter's Headstone")
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.AddItem(GameManager.Instance.woodenStakes);
                Inventory.instance.RemoveItem(SelectedItem, 1);
                questManager.CompleteTask(0);
                Quest quest3part4 = new Quest
                    (
                        "Go back to your house.",
                        new List<Task>
                        {
                            new Task("Go home.")
                        }
                    );

                questManager.AddQuest(quest3part4);
            }
          
        }else
        {
            Debug.Log(gameObject.name);
            Debug.Log(questManager.quests[0].questName);
            if(questManager.quests[0].questName == "Go back to your house." && gameObject.name == "MC's Door")
            {
                Debug.Log("GO HOME BUDDY");
                questManager.CompleteTask(0);
                
            }else  if(questManager.quests[0].questName == "Go back to your house" && gameObject.name == "MC's Door")
            {
                Debug.Log("GO HOME BUDDY");
                StartCoroutine(GameManager.Instance.DayTransition());
                questManager.CompleteTask(0);
                
            }else
            {
                StartCoroutine(InstantiateDialogue());
                dialogueAnimator.SetBool("FadeIn", true);
                if(GameManager.Instance.currentDay == 4 && gameObject.name == "MC's Door")
                {
                    interactLine = "I need to gather evidence.";
                }
                dialogueLine.text = interactLine;
            }
            
        }
        
    }

    public IEnumerator InstantiateDialogue()
    {

        yield return new WaitForSeconds(dialogueTime);
        
        dialogueAnimator.SetBool("FadeIn", false);
        
    }
}

