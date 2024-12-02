    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NPCInteract : MonoBehaviour
    {
        [Header("Managers")]
        public QuestManager questManager;
        public DialogueManager dialogueManager;
        public Inventory inventoryManager;

        [Header("Dialogues")]
        public Dialogue dialogueDefault;
        public Dialogue dialogueRepeat;
        public Dialogue dialogueQuest;
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
                FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueDefault);
                hasTalked = true;

            }
            if(gameObject.name == "Warung Owner")
            {
                if(Inventory.instance.IsItemSelected())
                {
                    Item SelectedItem = Inventory.instance.GetSelectedItem();

                    if((SelectedItem.itemName == "Filled Oil Can" || SelectedItem.itemName == "Filled Oil Can 1") )
                    {
                        inventoryManager.RemoveItem(SelectedItem);
                        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueQuest);
                        questManager.CompleteTask(0); 
                        if(dialogueManager.currSentence == "Here are the matches I promised you earlier!")
                        {
                            inventoryManager.AddItem(GameManager.Instance.Matches);
                            dialogueRepeat.dialogueLines[0].sentences = "Good luck with your investigation!";    
                        }
                       
                    }
                    
                }
            }
            
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
