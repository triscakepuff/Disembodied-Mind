using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject questCounter;
    public GameObject inventoryUI;

    [Header("Dialogue")]
    public bool startDialogue = false; // Private variable
    public bool StartDialogue // Public getter
    {
        get { return startDialogue; }
        set { startDialogue = value; } // Optional: Use only if you want to set it externally
    }
    public Dialogue dialogueAnanda;
    private bool dialogueAnandaBool;
    
    public Dialogue dialogueChief;
    private bool dialogueChiefBool;

    public Dialogue quest2Start;
    public List<GameObject> startingDialogues;
    public GameObject dialogueTrigger1;
    private int currentIndex = 0;
    public GameObject directions;
    public GameObject dialogueObject;

    [Header("Items")]
    public Item oilCan;
    public Item oilCan1;
    public Item filledOilCan;
    private bool oilCanTaken = false;
    private bool matchesTaken = false;
    public Item Matches;

    [Header("Managers")]
    public static GameManager Instance; // Singleton instance
    private NavigationManager navigationManager; // Reference to NavigationManager
    public DialogueManager dialogueManager;
    public QuestManager questManager;
    public Inventory inventoryManager;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startingDialogues[0].SetActive(true);
        startDialogue = true;
        dialogueManager.inDialogue = true; // Set to true when starting dialogue
        navigationManager = FindObjectOfType<NavigationManager>(); // Find the NavigationManager
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(dialogueManager.inDialogue);
        
        //Starting Dialogue at the beginning of the game

        if (startDialogue == false)
        {
            dialogueTrigger1.SetActive(true);
            questCounter.SetActive(true);
            inventoryUI.SetActive(true);
            directions.SetActive(true);
        }

        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ActivateNextText();
        }
        //Once we enter Ananda's zone
        if (navigationManager.cameras[3].gameObject.activeInHierarchy)
        {
            if(!dialogueAnandaBool)
            {   
                dialogueManager.StartDialogue(dialogueAnanda);
                dialogueAnandaBool = true;
            }
            
            questManager.CompleteTask(2);
        }
        if(navigationManager.cameras[6].gameObject.activeInHierarchy)
        {
            if(!dialogueChiefBool)
            {   
                dialogueManager.StartDialogue(dialogueChief);
                dialogueChiefBool = true;
            }
            
        }

        //Give Items
        if(dialogueManager.currSentence == "Take these two empty cans, one for me and one for you.")
        {
            if(!oilCanTaken)
            {
                inventoryManager.AddItem(oilCan);
                inventoryManager.AddItem(oilCan1);
                
                oilCanTaken = true;
            }
            
        }
        if (dialogueManager.currSentence == "Here are the matches I promised you earlier!")
        {
            if (!matchesTaken)
            {
                inventoryManager.AddItem(Matches);
                matchesTaken = true;
            }
           
            
        }

        //Trigger Dialogue
        // if(!questManager.IsQuestActive("Meet the Neighbours"))
        // {
        //     dialogueManager.StartDialogue(quest2Start);
        // }
    }

    private void ActivateNextText()
    {
        // Check if there are still text objects to activate
        if (currentIndex < startingDialogues.Count)
        {
            // Activate the current text object
            startingDialogues[currentIndex].SetActive(true);
            // Move to the next index
            currentIndex++;
        }
        else 
        {
            startDialogue = false;
            foreach (GameObject dialogue in startingDialogues)
            {
                Animator anim = dialogue.GetComponent<Animator>();
                anim.SetTrigger("FadeOut");
                StartCoroutine(DialogueFadeOut(dialogue, 1f));
            }

            // Notify NavigationManager that dialogue has ended
            if (navigationManager != null)
            {
                navigationManager.OnDialogueEnd();
            }
        }
    }

    private IEnumerator DialogueFadeOut(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(false);
    }

   

    public bool IsDialogueActive() // Method to check if dialogue is active
    {
        return startDialogue;
    }
}