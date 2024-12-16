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
    public GameObject Day5Text;
    public bool inTransition = false;
    public float waitTime = 4f;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public bool startDialogue = false;
    public bool day1StartDialogue = false;
    public bool day4StartDialogue = false;
    public bool endDialogueBoolDay4 = false;
    public bool StartDialogue // Public getter
    {
        get { return startDialogue; }
        set { startDialogue = value; } // Optional: Use only if you want to set it externally
    }
    public Dialogue dialogueAnanda;
    private bool dialogueAnandaBool;
    public Dialogue dialogueAnandaDay4;
    private bool dialogueAnandaDay4Bool;
    private bool anandaGivenQuest = false;

    public Dialogue dialogueChief;
    private bool dialogueChiefBool;
    public Dialogue dialogueChiefDay4;
    private bool dialogueChiefDay4Bool;

    public Dialogue dialogueNoShovel;

    public Dialogue dialogueShovel;

    public Dialogue dialogueKuyangHair;

    public Dialogue endDialogue;


    public Dialogue quest2Start;
    public GameObject[] startingDialogues;
    public GameObject dialogueTrigger1;
    public GameObject dialogueTrigger2;
    private int currentIndex = 0;
    public GameObject directions;
    public GameObject dialogueObject;

    [Header("Start Dialogues")]
    public string[] day1StartDialogues;
    public string[] day4StartDialogues;

    public GameObject startDialoguePrefab;
    public Transform startDialogueTransform;


    [Header("Items")]
    private bool notActivated = true;
    public GameObject flowersContainer;
    public GameObject kuyangHair;
    public GameObject shovelObject;
    public GameObject tornClothObject;
    public Item oilCan;
    public Item oilCan1;
    public Item filledOilCan;
    public Item flowers;
    public Item Matches;
    public Item hairpin;
    public Item bouquet;
    public Item woodenStakes;
    public Item shovel;
    public Item tornCloth;
    private bool oilCanTaken = false;
    private bool matchesTaken = false;
    private bool hairpinTaken = false;
    private bool shovelTaken = false;
    private bool hasBouquet = false;
    public bool kuyangHairTaken = false;
    private bool kuyangHairGone = false;
    private bool tornClothTaken = false;

    [Header("NPCs")]
    public GameObject Ananda;
    public GameObject Chief;

    [Header("Managers")]
    public static GameManager Instance; // Singleton instance
    public NavigationManager navigationManager; 
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
        InitializeItemCount();
        
        if (questManager == null)
        {
            // Automatically find QuestManager if not assigned in the Inspector
            questManager = FindObjectOfType<QuestManager>();
        }
        currentDay = 1;
        DayStartDialogue();
        startDialogue = true;
        navigationManager = FindObjectOfType<NavigationManager>(); // Find the NavigationManager
    }

    // Update is called once per frame
    void Update()
    {
        
        //Starting Dialogue at the beginning of the game

        if(!startDialogue && !dialogueManager.inDialogue)
        {
            dialogueTrigger1.SetActive(true);
            questCounter.SetActive(true);
            inventoryUI.SetActive(true);
            directions.SetActive(true);
            dialoguePanel.SetActive(false);
        }
        else
        {
            dialoguePanel.SetActive(true);
            questCounter.SetActive(false);
        }

        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !inTransition)
        {
            ActivateNextText();
        }
        //Once we enter Ananda's zone
        if (navigationManager.cameras[3].gameObject.activeInHierarchy)
        {
            if(!dialogueAnandaBool)
            {   
                dialogueManager.StartDialogue(dialogueAnanda);
                questManager.CompleteTask(1);
                dialogueAnandaBool = true;
            }
            
            if(!dialogueAnandaDay4Bool && currentDay == 4)
            {
                dialogueManager.StartDialogue(dialogueAnandaDay4);
                if (questManager.quests[0].questName == "Explore the village")
                {
                    questManager.CompleteTask(0);
                }
                if (!anandaGivenQuest)
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
                DayStartDialogue();
                day4StartDialogue = true;
            }
            
            if(!startingDialogues[0].activeInHierarchy)
            {
                dialogueTrigger2.SetActive(true);
            }

            Ananda.SetActive(true);
            Chief.SetActive(false);
            if (notActivated)
            {
                shovelObject.SetActive(true);
                flowersContainer.SetActive(true);
                notActivated = false;
            }
            

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

             if (dialogueManager.currSentence == "This is no longer just a hunt. It’s survival.")
            {
                Debug.Log("FUNCTION CALL");
            
            }

            if(questManager.quests[0].questName == "Investigate traces of the mess from the grave.")
            {
    
                Quest quest3Part4 = new Quest
                (
                    "Go back to your house.",
                    new List<Task>
                    {
                        new Task("Go home.")
                    }
                );
                questManager.AddQuest(quest3Part4);
            }
            

            //Activate kuyang Hair if player has shovel
            if (Inventory.instance.HasItem("Shovel"))
            {
                if(questManager.quests[0].questName == "Go back to your house.")
                {
                    if (kuyangHair != null)
                        kuyangHair.SetActive(true);

                    if (tornClothObject != null)
                        tornClothObject.SetActive(true);

                }

                if (questManager.quests[0].questName == "Investigate traces of the mess from the grave.")
                questManager.CompleteTask(0);
            }

           

        }
        //else if(currentDay == 1)
        // {
        //      if(!day1StartDialogue)
        //     {
        //         DayStartDialogue();
        //         day1StartDialogue = true;
        //     }
        // }
        
        //Check if shovel is taken or not
        if (Inventory.instance.HasItem("Shovel") && !shovelTaken)
        {
            dialogueManager.StartDialogue(dialogueShovel);
            
            shovelTaken = true;
        }

       

    }

    private void ActivateNextText()
    {
        Debug.Log("Current Index: " + currentIndex);
        // Check if there are still text objects to activate
        if (currentIndex < startingDialogues.Length)
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
            Day5Text.SetActive(true);
            currentDay = 5; 
        }
        if(currentDay == 5)
        {
            waitTime = 10f;
        }
        yield return new WaitForSeconds(waitTime);
        Day4Text.SetActive(false);
        anim.SetBool("FadeIn", false);
        inTransition = false;
   }

   public void DayStartDialogue()
   {
        questCounter.SetActive(false);
        inventoryUI.SetActive(false);
        directions.SetActive(false);
        startDialogue = true;
        
        if(currentDay == 1)
        {
            startingDialogues = new GameObject[day1StartDialogues.Length];
            for(int i = 0; i < startingDialogues.Length; i++)
            {
                var textComponent = startDialoguePrefab.GetComponent<TMPro.TextMeshProUGUI>();
                textComponent.text = day1StartDialogues[i];
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);
            
                startingDialogues[i] = newStartDialogue;

                startingDialogues[i].SetActive(false);
            }
        }else if(currentDay == 4)
        {
            startingDialogues = new GameObject[day4StartDialogues.Length];
              for(int i = 0; i < startingDialogues.Length; i++)
            {
                var textComponent = startDialoguePrefab.GetComponent<TMPro.TextMeshProUGUI>();
                textComponent.text = day4StartDialogues[i];
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);
            
                startingDialogues[i] = newStartDialogue;

                startingDialogues[i].SetActive(false);
            }
        }

         
        // Activate the first dialogue to start
        currentIndex = 0;
        ActivateNextText();
        
   }

    public void InitializeItemCount()
    {
        //Set each item count to 1
        oilCan.count = 1;
        oilCan1.count = 1;
        filledOilCan.count = 1;
        flowers.count = 1;
        bouquet.count = 1;
        hairpin.count = 1;
        woodenStakes.count = 1;
        shovel.count = 1;
        tornCloth.count = 1;
    }
    public bool IsDialogueActive() // Method to check if dialogue is active
    {
        return startDialogue;
    }
}