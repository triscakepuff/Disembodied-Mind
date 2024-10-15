    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NPCInteract : MonoBehaviour
    {
        public static NPCInteract Instance;
    

        [Header("Dialogues")]
        public GameObject dialogueComponent;
        public Dialogue dialogueDefault;
        public Dialogue dialogueEvidence;
        public Dialogue dialogue1;
        public Dialogue dialogue2;
        public Dialogue dialogue3;
        public Dialogue dialogueClimax;
        public string Evidence;

        [Header("Interrogation")]
        public bool shouldStartInterrogation = false;
        public bool CanBeInterrogated; // For interrogation mechanic
        public GameObject interrogationPanel;
        public GameObject obj1;
        public GameObject obj2;
        public GameObject obj3;
        private bool hasEndedInterrogation = false;
        private bool isInInterrogation = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        
        }

        void Update()
        {
            bool value1 = obj1.GetComponent<Interrogation>().Objection;
            bool value2 = obj2.GetComponent<Interrogation>().Objection;
            bool value3 = obj3.GetComponent<Interrogation>().Objection;
        
       
            if(value1 && value2 && value3 && !hasEndedInterrogation && !DialogueManager.Instance.IsDialogueActive())
            {
              hasEndedInterrogation = true;
              EndInterrogation();
            }
        
        }
        private void OnMouseDown()
        {
        
            Item selectedItem = Inventory.instance.GetSelectedItem();

            if (selectedItem != null && selectedItem.itemName == Evidence)
            {
                shouldStartInterrogation = true;
                DialogueManager.Instance.StartDialogue(dialogueEvidence);
            }else
            {
                DialogueManager.Instance.StartDialogue(dialogueDefault);
            }
        }
    
        public void StartInterrogation()
        {
     
            interrogationPanel.SetActive(true);
            isInInterrogation = true;
            hasEndedInterrogation = false;
      
        }

        public void PlayDialogue1()
        {
          interrogationPanel.SetActive(false);
          DialogueManager.Instance.StartDialogue(dialogue1);
        }

        public void PlayDialogue2()
        {
          interrogationPanel.SetActive(false);
          DialogueManager.Instance.StartDialogue(dialogue2);
        }

        public void PlayDialogue3()
        {
          interrogationPanel.SetActive(false);
          DialogueManager.Instance.StartDialogue(dialogue3);
        }

        public void EndInterrogation()
        {
            if(IsInInterrogation())
            interrogationPanel.SetActive(false);
            isInInterrogation = false;
            DialogueManager.Instance.StartDialogue(dialogueClimax);
        }

        public bool ShouldStartInterrogation()
        {
            return shouldStartInterrogation;
        }

        public bool IsInInterrogation()
        {
            return isInInterrogation;
        }
    }
