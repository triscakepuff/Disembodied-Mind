    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NPCInteract : MonoBehaviour
    {
        private bool hasTakenOil = false;
        private bool anandaGivenQuest = false;
        private bool isDay1 = true;
        [Header("Managers")]
        public QuestManager questManager;
        public DialogueManager dialogueManager;
        public Inventory inventoryManager;

        [Header("Dialogues")]
        public Dialogue dialogueDefault;
        public Dialogue dialogueRepeat;
        public Dialogue dialogueQuest;
        public Dialogue dialogueDay4;
        private bool hasTalked = false;
        public string Evidence;

        [Header("Interrogation")]
        public bool shouldStartInterrogation = false;
        public bool CanBeInterrogated; // For interrogation mechanic
        // public GameObject interrogationPanel;
        // public GameObject obj1;
        // public GameObject obj2;
        // public GameObject obj3;
        private bool hasEndedInterrogation = false;
        private bool isInInterrogation = false;
        private void Awake()
        {
        
        }

        void Update()
        {
            // bool value1 = obj1.GetComponent<Interrogation>().Objection;
            // bool value2 = obj2.GetComponent<Interrogation>().Objection;
            // bool value3 = obj3.GetComponent<Interrogation>().Objection;
        
       
            // if(value1 && value2 && value3 && !hasEndedInterrogation && !DialogueManager.Instance.IsDialogueActive())
            // {
            //   hasEndedInterrogation = true;
            //   EndInterrogation();
            // }
            if(GameManager.Instance.currentDay != 1 && isDay1)
            {
                ResetTalk();
            }
            if(hasTalked)
            {
                if(gameObject.name == "Agus")
                {
                    if(questManager.quests[0].questName == "Meet the Neighbours")
                        questManager.CompleteTask(1);
                }
                else if(gameObject.name == "Chief")
                {
                    if(questManager.quests[0].questName == "Meet the Neighbours")
                        questManager.CompleteTask(0);
                }
                else if(gameObject.name == "Warung Owner")
                {
                    
                    if(questManager.quests[0].questName == "Meet the Owner of the food stall")
                    {
                        questManager.CompleteTask(0);   
                    }

                       
                }
            }

            
        }
        private void OnMouseDown()
        {
            Debug.Log(gameObject.name);
            Item selectedItem = Inventory.instance.GetSelectedItem();

            if (hasTalked)
            {
                FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueRepeat);
                
                
            }else
            {
                if(GameManager.Instance.currentDay == 4)
                {
                    if(gameObject.name == "Warung Owner")
                    {
                        dialogueRepeat.dialogueLines[0].sentences = "Once again, good luck with your investigation!";
                    }else if(gameObject.name == "Agus")
                    {
                        dialogueRepeat.dialogueLines[0].sentences = "I need to hurry";
                    }
                     
                    FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDay4);
                    hasTalked = true;

                }else if(GameManager.Instance.currentDay == 1)
                {
                    FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDefault);
                    hasTalked = true;
                }
               

            }
            if(gameObject.name == "Warung Owner")
            {
                if(Inventory.instance.IsItemSelected())
                {
                    Item SelectedItem = Inventory.instance.GetSelectedItem();

                    if(SelectedItem.itemName == "Filled Oil Can" && !hasTakenOil)
                    {
                        Inventory.instance.DeselectItem();
                        Debug.Log("QUEST COMPLETED");
                        inventoryManager.RemoveItem(SelectedItem, 1);
                        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuest);
                        questManager.CompleteTask(1);
                        dialogueRepeat.dialogueLines[0].sentences = "Good luck with your investigation!";                    }
                        hasTakenOil = true;
                    
                }
            }
            else if(gameObject.name == "Ananda")
            {
                Item SelectedItem = Inventory.instance.GetSelectedItem();
                if(questManager.quests[0].questName == "Explore the village")
                {
                    questManager.CompleteTask(0);   
                }
                if(!anandaGivenQuest)
                {
                    Quest quest3part2 = new Quest
                    (
                        "Comfort Ananda with flowers",
                        new List<Task>
                        {
                            new Task("Collect 10 flowers for a bouquet"),
                            new Task("Give the bouquet to Ananda")
                        }
                    );
                    questManager.AddQuest(quest3part2);
                   anandaGivenQuest = true;
                }
                
                if(Inventory.instance.IsItemSelected())
                {
                    if(selectedItem.itemName == "Bouquet")
                    {
                        Inventory.instance.DeselectItem();
                        inventoryManager.RemoveItem(SelectedItem, 1);
                        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuest);
                        questManager.CompleteTask(1);
                        dialogueRepeat.dialogueLines[0].sentences = "Thank you, once again.";
                        dialogueRepeat.dialogueLines[0].name = "Ananda";   

                        Quest quest2part3 = new Quest
                        (
                            "Visit Ananda’s daughter’s grave",
                            new List<Task>
                            {
                                new Task("Take the wooden stake from her grave")
                            }
                        );

                    
                    questManager.AddQuest(quest2part3);
                    }
                }
            }
            
        }

        public void ResetTalk()
        {
            hasTalked = false;
            isDay1 = false;
        }
    
        // public void StartInterrogation()
        // {
     
        //     interrogationPanel.SetActive(true);
        //     isInInterrogation = true;
        //     hasEndedInterrogation = false;
      
        // }

        // public void PlayDialogue1()
        // {
        //   interrogationPanel.SetActive(false);
        //   DialogueManager.Instance.StartDialogue(dialogue1);
        // }

        // public void PlayDialogue2()
        // {
        //   interrogationPanel.SetActive(false);
        //   DialogueManager.Instance.StartDialogue(dialogue2);
        // }

        // public void PlayDialogue3()
        // {
        //   interrogationPanel.SetActive(false);
        //   DialogueManager.Instance.StartDialogue(dialogue3);
        // }

        // public void EndInterrogation()
        // {
        //     if(IsInInterrogation())
        //     interrogationPanel.SetActive(false);
        //     isInInterrogation = false;
        //     DialogueManager.Instance.StartDialogue(dialogueClimax);
        // }

        // public bool ShouldStartInterrogation()
        // {
        //     return shouldStartInterrogation;
        // }

        // public bool IsInInterrogation()
        // {
        //     return isInInterrogation;
        // }
    }
