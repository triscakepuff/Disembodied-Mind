using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int currentDay;
    public GameObject questCounter;
    public GameObject inventoryUI;
    public GameObject screenPanel;
    public GameObject Day1Text;
    public GameObject Day4Text;
    public bool inTransition = false;

    [Header("Dialogue")]
    public bool startDialogue = false;
    public bool day4StartDialogue = false;
    public bool StartDialogue // Public getter
    {
        get { return startDialogue; }
        set { startDialogue = value; } // Optional: Use only if you want to set it externally
    }
    public Dialogue dialogueAnanda;
    private bool dialogueAnandaBool;
    public Dialogue dialogueAnandaDay4;
    private bool dialogueAnandaDay4Bool;

    public Dialogue dialogueChief;
    private bool dialogueChiefBool;
    public Dialogue dialogueChiefDay4;
    private bool dialogueChiefDay4Bool;

    public Dialogue quest2Start;
    public List<GameObject> startingDialogues;
    public GameObject dialogueTrigger1;
    public GameObject dialogueTrigger2;
    private int currentIndex = 0;
    public GameObject directions;
    public GameObject dialogueObject;

    public string[] day4StartDialogues;

    [Header("Items")]
    public Item oilCan;
    public Item oilCan1;
    public Item filledOilCan;
    public Item flowers;
    public Item Matches;
    public Item hairpin;
    public Item bouquet;
    public Item woodenStakes;
    private bool oilCanTaken = false;
    private bool matchesTaken = false;
    private bool hairpinTaken = false;
    private bool hasBouquet = false;

    [Header("NPCs")]
    public GameObject Ananda;
    public GameObject Chief;

    [Header("Managers")]
    public static GameManager Instance; // Singleton instance
    private NavigationManager navigationManager; // Reference to NavigationManager
    public DialogueManager dialogueManager;
    public QuestManager questManager;
    public Inventory inventoryManager;
    private Animator anim;
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
        //Set each item count to 1
        oilCan.count = 1;
        oilCan1.count = 1;
        filledOilCan.count = 1;
        flowers.count = 1;
        bouquet.count = 1;
        hairpin.count = 1;
        woodenStakes.count = 1;
        if (questManager == null)
        {
            // Automatically find QuestManager if not assigned in the Inspector
            questManager = FindObjectOfType<QuestManager>();
        }
        currentDay = 1;
        startingDialogues[0].SetActive(true);
        startDialogue = true;
        navigationManager = FindObjectOfType<NavigationManager>(); // Find the NavigationManager
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(startDialogue);
        
        //Starting Dialogue at the beginning of the game

        if(!startDialogue)
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
                questManager.CompleteTask(2);
                dialogueAnandaBool = true;
            }
            
            if(!dialogueAnandaDay4Bool && currentDay == 4)
            {
                dialogueManager.StartDialogue(dialogueAnandaDay4);
                dialogueAnandaDay4Bool = true;
            }

        }
        if(navigationManager.cameras[6].gameObject.activeInHierarchy)
        {
            if(currentDay == 1)
            {
                if(!dialogueChiefBool)
                {   
                    dialogueManager.StartDialogue(dialogueChief);
                    dialogueChiefBool = true;
                }
            }
            else if(currentDay == 4)
            {
                if(!dialogueChiefDay4Bool)
                {   
                    dialogueManager.StartDialogue(dialogueChiefDay4);
                    dialogueChiefDay4Bool = true;
                }
            }
            
            
        }

        //Give Items at Stall Owner's house
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

        //Once we enter Day 4
        if(currentDay == 4)
        {
            if(!day4StartDialogue)
            {
                Day4Dialogue();
                day4StartDialogue = true;
            }
            
            if(!startingDialogues[0].activeInHierarchy)
            {
                dialogueTrigger2.SetActive(true);
            }
            Ananda.SetActive(true);
            Chief.SetActive(false);
            if(flowers.count == 10)
            {
                inventoryManager.RemoveItem(flowers, 10);
                inventoryManager.AddItem(bouquet);
                hasBouquet = true;
            }
            //Check for Jasmine flowers count
            if(questManager.quests[0].questName == "Comfort Ananda with flowers" && hasBouquet)
            {
                questManager.CompleteTask(0);
           
            }

            if(dialogueManager.currSentence == "Well…. you can take this hairpin and use it as an offering")
            {
                if(!hairpinTaken)
                {
                    inventoryManager.AddItem(hairpin);
                    
                    hairpinTaken = true;
                }
                
            }

            if(questManager.quests[0].questName == "Go back to your house.")
            {
                if(!Inventory.instance.HasItem("Shovel"))
                {
                    //StartCoroutine(DayTransition());
                    Quest quest3Part5 = new Quest
                    (
                        "Investigate traces of the mess from the grave.",
                        new List<Task>
                        {
                            new Task("Find the evidence that created this mess.")
                        }
                    );

                    questManager.AddQuest(quest3Part5); 
                
                }
                    
            }
        }

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
            ExitDialogue();
        }
    }

    public void ExitDialogue()
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
    private IEnumerator DialogueFadeOut(GameObject target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(false);
    }

   public IEnumerator DayTransition()
   {
        inTransition = true;
        Debug.Log("Function Called");
        anim = screenPanel.GetComponent<Animator>();
        anim.SetBool("FadeIn", true);

        yield return new WaitForSeconds(2f);
        
        if(currentDay == 1)
        {
            Day4Text.SetActive(true);
            currentDay = 4;
            Quest quest3part1 = new Quest
            (
                "Explore the village",
                new List<Task>
                {
                    new Task("Explore the village for clues")
                }
            );
            questManager.AddQuest(quest3part1);
        }else if(currentDay == 4)
        {
            //Day5Text.SetActive(true);
            currentDay = 5;
        }
        yield return new WaitForSeconds(4f);
        Day4Text.SetActive(false);
        anim.SetBool("FadeIn", false);
        inTransition = false;
   }

   public void Day4Dialogue()
   {
        Debug.Log("DAY 4 CALLED");
        questCounter.SetActive(false);
        inventoryUI.SetActive(false);
        directions.SetActive(false);
        startDialogue = true;

        // Ensure the texts align with the dialogues
        for (int i = 0; i < startingDialogues.Count && i < day4StartDialogues.Length; i++)
        {
            startingDialogues[i].GetComponent<TextMeshProUGUI>().text = day4StartDialogues[i];
            startingDialogues[i].SetActive(false); // Make sure they're inactive initially
        }

        // Activate the first dialogue to start
        currentIndex = 0;
        ActivateNextText();
   }

    public bool IsDialogueActive() // Method to check if dialogue is active
    {
        return startDialogue;
    }
}