  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    private bool hasTakenOil = false;
    private bool hasTakenQuill = false;
    private bool eventDialogue = false;
    private bool talkedPostInterrogation = false;
    private int lastKnownDay = 1;
    [Header("Managers")]
    public QuestManager questManager;
    public DialogueManager dialogueManager;
    public Inventory inventoryManager;

    [Header("Dialogues")]
    public Dialogue dialogueDay1;
    public Dialogue dialogueDay2;
    public Dialogue dialogueDay3;
    public Dialogue dialogueRepeat;
    public Dialogue dialogueQuestDay1;
    public Dialogue dialogueQuestDay2;
    public Dialogue dialogueQuestDay3;
    public Dialogue dialogueQuestDay31;
    public Dialogue dialogueQuestDay32;
    public Dialogue dialogueQuestDay4;
    public Dialogue dialogueQuestDay5;
    public Dialogue dialogueDay4;
    public Dialogue dialogueDay5;
    public Dialogue dialogueEvent;
    public bool hasTalked = false;
    public string Evidence;

    [Header("Interrogation")]
    public bool startInterrogation = false;
    public bool startInterrogation2 = false;
    public bool CanBeInterrogated; // For interrogation mechanic
    private bool hasEndedInterrogation = false;
    private bool isInInterrogation = false;
    public GameObject interrogationPanels;
    public GameObject interrogationPanelPoorMan;
    public GameObject interrogationPanelChief;
    private Collider2D collider;
    private bool hasTriggeredPostInterrogationDialogue = false;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        TimeManager timeManager = FindObjectOfType<TimeManager>();
    }
    void Update()
    {
       if (GameManager.Instance.currentDay != lastKnownDay)
        {
            ResetTalk();
            lastKnownDay = GameManager.Instance.currentDay; // Update the last known day
        }

        if(GameManager.Instance.currentDay != 2 && startInterrogation)
        {
            startInterrogation = false;
        }
        if (hasTalked)
        {
            if (gameObject.name == "Agus")
            {
                if(questManager.quests.Count > 0)
                {
                if (questManager.quests[0].questName == "Meet the Neighbours")
                    questManager.CompleteTask(2);
                }
            }
            else if (gameObject.name == "Chief")
            {
                if(questManager.quests.Count > 0)
                {
                if (questManager.quests[0].questName == "Meet the Neighbours")
                    questManager.CompleteTask(0);
                }
            }
            else if (gameObject.name == "Warung Owner")
            {
                if(questManager.quests.Count > 0)
                {
                if (questManager.quests[0].questName == "Meet the Owner of the food stall")
                    questManager.CompleteTask(0);
                
                }
            
            }
        }

        if (dialogueManager.inDialogue || InterrogationManager.Instance.currentPanel.activeInHierarchy)
        {
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }

        //Interrogation Manager
        if(isInInterrogation)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                interrogationPanels.SetActive(false);
            }
        }
        
        if(dialogueManager.currSentence == "LIKE I SAID, I DIDN'T STEAL ANYTHING!!!" && !dialogueManager.inDialogue && !startInterrogation)
        {
            StartInterrogation();
            startInterrogation = true;
        }

        if(dialogueManager.currSentence == "GET LOST!!!" && !dialogueManager.inDialogue && !startInterrogation2)
        {
            StartInterrogation();
            startInterrogation2 = true;
        }

       
    }

    private void OnMouseDown()
    {
        if(gameObject.name == "Agus") Debug.Log(hasTalked);
        
        Item selectedItem = Inventory.instance.GetSelectedItem();

        
        // Triggering normal dialogue
        if (hasTalked && !GameManager.Instance.inEvent && !InterrogationManager.Instance.interrogationCompleted && !InterrogationManager.Instance.inInterrogation)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueRepeat);
        }else if(GameManager.Instance.inEvent)
        {
            Debug.Log("Function Called");
            HandleEventDialogue();
        }else if(InterrogationManager.Instance.inInterrogation && !interrogationPanels.activeInHierarchy)
        {
            questManager.CompleteTask(0);
            StartInterrogation();
        }else if(InterrogationManager.Instance.interrogationCompleted && !talkedPostInterrogation)
        {
            HandlePostInterrogationDialogue();
            talkedPostInterrogation = true;
        }
        else
        {
            HandleFirstDialogue();
        }

        HandleNPCBehaviour();
        
    }
    private void HandleFirstDialogue()
    {
        if (GameManager.Instance.currentDay == 1)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay1);
            hasTalked = true;
        }
        else if (GameManager.Instance.currentDay == 2)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay2);

            //Warung Owner Day 2 Logic
            if(gameObject.name == "Warung Owner")
            {
                questManager.CompleteTask(0);
                Quest quest2Part2 = new Quest
                (
                    "Track porcupines in the forest",

                    new List<Task>
                    {
                        new Task("Obtain 2 porcupine quills")
                    }
                );

                questManager.AddQuest(quest2Part2); 
                dialogueRepeat.dialogueLines[0].sentences = "Once you've got the quill, bring it to me.";
                
            }
            
            //Poor Man Day 2 Logic
            if(gameObject.name == "Poor Man")
            {
                questManager.CompleteTask(0);

            }
            hasTalked = true;     
        }else if(GameManager.Instance.currentDay == 3)
        {
            if(gameObject.name == "Agus")
            {
                if(questManager.quests[0].questName == "Bring water to the poor man")
                {
                    FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay3);
                    dialogueRepeat.dialogueLines[0].sentences = "Once you've got the wood, bring it to me.";
                }
                hasTalked = true;
            }else
            {
                FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay3);
                hasTalked = true;
            }
            
        }
        else if (GameManager.Instance.currentDay == 4)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay4);
            hasTalked = true;
        }else if(GameManager.Instance.currentDay == 5)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay5);
            hasTalked = true;

            if(gameObject.name == "ChiefInFoodStall")
            {
                GameManager.Instance.poorMan.SetActive(false);
            }
        }

        
    }
    private void HandleDialogueQuest()
    {
        Item SelectedItem = Inventory.instance.GetSelectedItem();
        Inventory.instance.DeselectItem();
        Debug.Log("QUEST COMPLETED");
        inventoryManager.RemoveItem(SelectedItem, 1);

        if (GameManager.Instance.currentDay == 1)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuestDay1);
        }else if(GameManager.Instance.currentDay == 2)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuestDay2);
        }
    }

    private void HandleEventDialogue()
    {
        // Triggering event dialogue

        //Event dialogue Warung Owner on Day 2
        if(questManager.quests[0].questName == "Investigate what happened at the food stall" && gameObject.name == "Warung Owner")
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueEvent);
            dialogueRepeat.dialogueLines[0].sentences = "I think he might be hiding in the abandoned house near here.";
            dialogueRepeat.dialogueLines[0].name = "Warung Owner";
            GameManager.Instance.inEvent = false;
            questManager.CompleteTask(0);

            Quest quest2Part5 = new Quest
            (
                "Find the poor man",
                new List<Task>
                {
                    new Task("Visit him at the abandoned house")
                }
            );

            questManager.AddQuest(quest2Part5);
            GameManager.Instance.poorMan.SetActive(true);
            GameManager.Instance.stolenQuill.SetActive(true);
        }
    }

    private void HandlePostInterrogationDialogue()
    {
        Item SelectedItem = Inventory.instance.GetSelectedItem();
        if(gameObject.name == "Warung Owner" && SelectedItem.itemName == "Quills")
        {
            Inventory.instance.DeselectItem();
            inventoryManager.RemoveItem(GameManager.Instance.Quills, 1);
            GameManager.Instance.talkedPostInterrogation = true;
            FindAnyObjectByType<DialogueManager>().StartDialogue(GameManager.Instance.dialogueOwnerAfterInterrogation);
        }                       
    }
    public void ResetTalk()
    {
        hasTalked = false;
    }

    public void StartInterrogation()
    {
        interrogationPanels.SetActive(true);
        InterrogationManager.Instance.currentPanel.SetActive(true);
       
        InterrogationManager.Instance.inInterrogation = true;
        hasEndedInterrogation = false;
    
    }

    public void HandleNPCBehaviour()
    {
        // Giving Items to NPCs logic or NPCs giving item to us
        if (gameObject.name == "Warung Owner")
        {
            if (Inventory.instance.IsItemSelected())
            {
                Item SelectedItem = Inventory.instance.GetSelectedItem();

                if (SelectedItem.itemName == "Filled Oil Can" && !hasTakenOil)
                {
                    HandleDialogueQuest();
                    if(questManager.quests.Count > 0)
                    questManager.CompleteTask(1);
                    
                    dialogueRepeat.dialogueLines[0].sentences = "Thank you and good luck with your investigation!";
                    dialogueRepeat.dialogueLines[0].name = "Warung Owner";
                    hasTakenOil = true;
                    TimeManager.Instance.ShiftTime("Afternoon");
                }else  if (SelectedItem.itemName == "Quills" && !hasTakenQuill)
                { 
                    Debug.Log("Function called");
                    HandleDialogueQuest();
                    questManager.CompleteTask(0);
                    dialogueRepeat.dialogueLines[0].sentences = "Thank you and good luck with your investigation!";
                    dialogueRepeat.dialogueLines[0].name = "Warung Owner";
                    hasTakenQuill = true;
                    
                }
            }
        }
        else if (gameObject.name == "Ananda")
        {
            
            if (Inventory.instance.IsItemSelected())
            {
                Item selectedItem = Inventory.instance.GetSelectedItem();

                if (selectedItem.itemName == "Bouquet")
                {
                    Inventory.instance.DeselectItem();
                    inventoryManager.RemoveItem(selectedItem, 1);
                    FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuestDay4);
                    questManager.CompleteTask(1);
                    dialogueRepeat.dialogueLines[0].sentences = "Thank you, once again.";

                    Quest quest4part3 = new Quest
                    (
                        "Visit Ananda’s daughter’s grave",
                        new List<Task>
                        {
                            new Task("Take the wooden stake from her grave")
                        }
                    );

                    questManager.AddQuest(quest4part3);
                }
            }
        }else if(gameObject.name == "Poor Man")
        {
            if (Inventory.instance.IsItemSelected())
            {
                Item selectedItem = Inventory.instance.GetSelectedItem();

                if (selectedItem.itemName == "Stolen Quill")
                {
                    Inventory.instance.DeselectItem();
                    dialogueManager.StartDialogue(dialogueQuestDay2);
                    
                }

            }
            //To activate interrogation panel back
            if(isInInterrogation)
            {
                if(!interrogationPanels.activeInHierarchy)
                {
                    InterrogationManager.Instance.currentPanel.SetActive(true);
                }
            }

            if(GameManager.Instance.currentDay == 3)
            {
                dialogueRepeat.dialogueLines[0].sentences = "If you have the water, just leave it at the front door.";
                 dialogueRepeat.dialogueLines[0].name = "Poor Man";
            }
        }else if(gameObject.name == "Agus")
        {
            if (Inventory.instance.IsItemSelected())
            {
                Item selectedItem = Inventory.instance.GetSelectedItem();

                if (selectedItem.itemName == "Log")
                {
                    Inventory.instance.DeselectItem();
                    Inventory.instance.DeselectCombinationItems();
                    dialogueManager.StartDialogue(dialogueQuestDay31);
                }else if(selectedItem.itemName == "Wood")
                {
                    Inventory.instance.DeselectItem();
                    Inventory.instance.RemoveItem(selectedItem, 1);
                    dialogueManager.StartDialogue(dialogueQuestDay32);
                    dialogueRepeat.dialogueLines[0].sentences = "Good Luck!";
                }

            }
        }
    }
}
     
