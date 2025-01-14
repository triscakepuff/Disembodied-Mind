using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int currentDay = 1;
    public GameObject questCounter;
    public GameObject inventoryUI;
    public GameObject screenPanel;
    public GameObject noteCluePanel;
    public GameObject confessionNotePanel;
    public GameObject safePanel;
    public GameObject Day1Text;
    public GameObject Day2Text;
    public GameObject Day3Text;
    public GameObject Day4Text;
    public GameObject Day5Text;
    private GameObject DayText;
    public GameObject interrogationPanelChief;
     public GameObject interrogationPanelPoorMan;
    public bool inTransition = false;
    public float waitTime = 4f;
    private bool day1InterrogationTarget = false;
    private bool day3InterrogationTarget = false;
    private bool day5InterrogationTarget = false;

    [Header("Safe Game Objects")]
    public TMP_Text[] numberObjects;
    public int[] numbers;
    public GameObject safe;
    public bool unlocked = false;
    public bool lockSelected = false;
    public Sprite lockOpen;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public bool startDialogue = false;
    public bool endDialogueBoolDay4 = false;
    public bool endDialogueDay2 = false;
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

    private bool Day2GivenQuest = false;
    private bool Day3GivenQuest = false;
    private bool Day3GivenQuest2 = false;
    private bool Day4GivenQuest = false;
    private bool Day4GivenQuest2 = false;
    private bool Day5GivenQuest = false;
    private bool Day5GivenQuest2 = false;
    private bool Day5GivenQuest3 = false;
    private bool enteredAbandonedHouse = false;
    public Dialogue poorManInterrogation;

    public Dialogue dialogueChief;
    private bool dialogueChiefBool;
    public Dialogue dialogueChiefDay4;
    public Dialogue dialogueChiefDay5;

    public Dialogue dialogueAgusDay5;

    public Dialogue dialogueOwnerAfterInterrogation;
    private bool dialogueChiefDay4Bool;
    private bool dialogueChiefDay5Bool;
    private bool dialogueAgusDay5Bool;

    public Dialogue dialogueNoShovel;

    public Dialogue dialogueKuyangHair;
    public Dialogue dialogueTornCloth;
    private bool dialogueKuyangHairBool = false;
    private bool dialogueTornClothBool = false;
    public Dialogue dialogueShovel;
    public string[] endDialogue;


    //Interrogation Dialogues
    public Dialogue dialoguePoorManFinished;
    public Dialogue dialogueChiefFinished;
    public Dialogue questDay4Start;
    public GameObject[] startingDialogues;
    public GameObject dialogueTrigger1;
    public GameObject dialogueTrigger11;
    public GameObject dialogueTrigger2;
    public GameObject dialogueTrigger21;
    public GameObject dialogueTrigger22;
    public GameObject dialogueTrigger23;
    public GameObject dialogueTrigger24;
    public GameObject dialogueTrigger25;
    public GameObject dialogueTrigger26;
    public GameObject dialogueTrigger3;
    public GameObject dialogueTrigger31;
    public GameObject dialogueTrigger32;
    public GameObject dialogueTrigger33;
    public GameObject dialogueTrigger34;
    public GameObject dialogueTrigger35;
    public GameObject dialogueTrigger4;
    public GameObject dialogueTrigger41;
    public GameObject dialogueTrigger42;
    public GameObject dialogueTrigger43;   
    public GameObject dialogueTrigger5;
    public GameObject dialogueTrigger51;
    public GameObject dialogueTrigger52;
    private int currentIndex = 0;
    public GameObject directions;
    public GameObject dialogueObject;

    [Header("Start Dialogues")]
    public string[] day1StartDialogues;
    public string[] day2StartDialogues;
    public string[] day3StartDialogues;
    public string[] day4StartDialogues;
    public string[] day5StartDialogues;

    public bool day1StartDialogue = false;
    public bool day2StartDialogue = false;
    public bool day3StartDialogue = false;
    public bool day4StartDialogue = false;
    public bool day5StartDialogue = false;

    public GameObject startDialoguePrefab;
    public Transform startDialogueTransform;
    public GameObject startDialogueScreen;

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
    public Item Gloves;
    public Item Quills;
    public Item StolenQuill;
    public Item Axe;
    public Item Log;
    public Item Wood;
    public Item Pulley;
    public Item EmptyBucket;
    public Item FilledBucket;
    public Item ConfessionNote;
    public Item BoltCutter;
    private bool oilCanTaken = false;
    private bool matchesTaken = false;
    private bool hairpinTaken = false;
    private bool shovelTaken = false;
    private bool shovelTaken2 = false;
    private bool hasBouquet = false;
    public bool kuyangHairTaken = false;
    private bool glovesTaken = false;
    private bool axeTaken = false;
    private bool axeTaken2 = false;
    private bool pulleyTaken = false;
    private bool waterBucketGiven = false;
    public bool hasGivenQuill;
    public bool quillHammered;

    [Header("NPCs")]
    public GameObject warungOwner;
    public GameObject Ananda;
    public GameObject Chief;
    public GameObject Agus;
    public GameObject foodStallNPCs;
    public GameObject poorMan;
    public GameObject ChiefInFoodStall;

    [Header("Objects")]
    public GameObject ladder;
    public GameObject stolenQuill;
    public GameObject abandonedHouseDoor;
    public GameObject BucketSpot;
    public GameObject waterBucket;
    public GameObject quill;
    public GameObject PulleyOnScene;
    public GameObject clueNote;
    public GameObject quillPoorMan;
    public GameObject porcupine;
    public GameObject porcupine2;
    public GameObject mcDoor;
    public GameObject warungDoor;

    public bool placedLadder = false;
    public bool doorUnlocked = false;
    private bool noteSelected = false;
    private bool seenConfessionNote = false;

    public bool fenceCut = false;
    public bool hatchOpened = false;

    public bool putStake = false;
    public bool putJelangkung = false;
    public bool putOil = false;
    [Header("Events")]
    private bool eventTriggered = false;
    public bool inEvent = false;
    private bool warungOwnerScream = false;
    public bool ownerGotStolen = false;
    private bool jumpscareStarted = false;
    private bool quillStolen = false;
    public bool talkedPostInterrogation = false;
    private bool postInterrogation = false;
    private bool day3Survive = false;
    private bool day5Survive = false;
    public bool kuyangSetOnFire = false;
    private float jumpscareTime;
    public float day3JumpscareCooldown = 15f;
    public float day5JumpscareCooldown = 40f;
    public bool inJumpscareEvent = false;
    private bool startCountdown = false;
    public GameObject ghostEyes;

    private bool day3Transition = false;

    public Sprite wellDown;
    public Sprite wellUp;
    public int objectAction = 0;

    [Header("Time")]
    private bool day2ChangedTime = false;
    private bool day3ChangedTime = false;
    private bool day4ChangedTime = false;
    private bool day5ChangedTime = false;

    [Header("Managers")]
   
    public NavigationManager navigationManager; 
    public DialogueManager dialogueManager;
    public QuestManager questManager;
    public Inventory inventoryManager;
    private Animator anim;

    [Header("Ghost")]
    public GameObject kuyangHover;
    public GameObject kuyangJumpscare;
    private Animator animatorKuyang;

    private float kuyangHoverSpeed;
    public float day3FloatSpeed;
    public float day5FloatSpeed;

    [Header("Cutscene")]
    public GameObject chiefCutscene;
    private VideoPlayer videoPlayer; 
    public GameObject videoCanvas;

    public GameObject chiefDead;
    public GameObject chiefKeyOnScene;
    private bool cutscene1 = false;
    private bool cutscene2 = false;

    public GameObject endingCutscene;
    private VideoPlayer videoPlayer2;
    public GameObject videoCanvas2;
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
        TimeManager.Instance.ShiftTime("Noon");
        // inventoryManager.AddItem(tornCloth);
        // inventoryManager.AddItem(shovel);
        // inventoryManager.AddItem(Axe);
        // inventoryManager.AddItem(woodenStakes);
        // inventoryManager.AddItem(Matches);
        // inventoryManager.AddItem(filledOilCan);
        InitializeItemCount();
        UpdateAllDisplays();
        if (questManager == null)
        {
            // Automatically find QuestManager if not assigned in the Inspector
            questManager = FindObjectOfType<QuestManager>();
        }
        currentDay = 3;
        DayStartDialogue();
        startDialogue = true;
        navigationManager = FindObjectOfType<NavigationManager>(); // Find the NavigationManager
        FindObjectOfType<AudioManager>().Play("Ambience Day");
        animatorKuyang = kuyangHover.GetComponent<Animator>(); 
        videoPlayer = chiefCutscene.GetComponent<VideoPlayer>();
        videoPlayer2 = endingCutscene.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(numbers[0]);
        Debug.Log(numbers[1]);
        Debug.Log(numbers[2]);
        Debug.Log(unlocked);
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !inTransition)
        {
            if(inEvent) return;
            ActivateNextText();
        }
        if(!startDialogue)
        { 
            ActivateUI(); startDialogueScreen.SetActive(false);
        } 
        else 
        { 
            DeactivateUI(); if(!eventTriggered) startDialogueScreen.SetActive(true);
        }
        if(currentDay == 1)
        {
            HandleDay1();
        }else if(currentDay == 2)
        {
            HandleDay2();
        }else if(currentDay == 3)
        {
            HandleDay3();
        }
        //Once we enter Day 4
        else if(currentDay == 4)
        {  
            HandleDay4();
        }
        //Once we enter Day 5
        else if(currentDay == 5)
        {
            HandleDay5();
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
        DeactivateUI();
        anim.SetBool("FadeIn", true);

        yield return new WaitForSeconds(2f);
        
        switch (currentDay)
        {
            case 1:
                Day2Text.SetActive(true);
                DayText = Day2Text;
                currentDay = 2;
                break;
            case 2:
                Day3Text.SetActive(true);
                DayText = Day3Text;
                currentDay = 3;
                break;
            case 3:
                Day4Text.SetActive(true);
                DayText = Day4Text;
                currentDay = 4;
                break;
            case 4:
                Day5Text.SetActive(true);
                DayText = Day5Text;
                currentDay = 5;
                break;
            default:
                Debug.LogError("Unexpected day value: " + currentDay);
                break;
        }

        yield return new WaitForSeconds(waitTime);
        DayText.SetActive(false);
        anim.SetBool("FadeIn", false);
        inTransition = false;
   }

   public void DayStartDialogue()
   {
        DeactivateUI();
        startDialogue = true;
        
        // Clear previously instantiated dialogues
        if (startingDialogues != null)
        {
            foreach (var dialogue in startingDialogues)
            {
                if (dialogue != null)
                {
                    Destroy(dialogue); // Remove old dialogue objects
                }
            }
        }
        if(currentDay == 1)
        {
           startingDialogues = new GameObject[day1StartDialogues.Length];
            for (int i = 0; i < day1StartDialogues.Length; i++)
            {
                // Instantiate the prefab first
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

                // Set the text of the instantiated object
                var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = day1StartDialogues[i];
                }

                // Add to the array
                startingDialogues[i] = newStartDialogue;
                startingDialogues[i].SetActive(false);
            }
        }else if(currentDay == 2)
        {
            startingDialogues = new GameObject[day2StartDialogues.Length];
            for (int i = 0; i < day2StartDialogues.Length; i++)
            {
                // Instantiate the prefab first
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

                // Set the text of the instantiated object
                var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = day2StartDialogues[i];
                }

                // Add to the array
                startingDialogues[i] = newStartDialogue;
                startingDialogues[i].SetActive(false);
            }
        }else if(currentDay == 3)
        {
            startingDialogues = new GameObject[day3StartDialogues.Length];
            for (int i = 0; i < day3StartDialogues.Length; i++)
            {
                // Instantiate the prefab first
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

                // Set the text of the instantiated object
                var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = day3StartDialogues[i];
                }

                // Add to the array
                startingDialogues[i] = newStartDialogue;
                startingDialogues[i].SetActive(false);
            }
        }else if(currentDay == 4)
        {
            startingDialogues = new GameObject[day4StartDialogues.Length];
            for (int i = 0; i < day4StartDialogues.Length; i++)
            {
                // Instantiate the prefab first
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

                // Set the text of the instantiated object
                var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = day4StartDialogues[i];
                }

                // Add to the array
                startingDialogues[i] = newStartDialogue;
                startingDialogues[i].SetActive(false);
            }
        }else if(currentDay == 5)
        {
            startingDialogues = new GameObject[day5StartDialogues.Length];
            for (int i = 0; i < day5StartDialogues.Length; i++)
            {
                // Instantiate the prefab first
                GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

                // Set the text of the instantiated object
                var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = day5StartDialogues[i];
                }

                // Add to the array
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
        Quills.count = 1;
        Axe.count = 1;
        Log.count = 1;
        Wood.count = 1;
        Pulley.count = 1;
        ConfessionNote.count = 1;
    }

    public void DeactivateUI()
    {
        questCounter.SetActive(false);
        inventoryUI.SetActive(false);
        directions.SetActive(false);
    }

    public void ActivateUI()
    {
        questCounter.SetActive(true);
        inventoryUI.SetActive(true);
        directions.SetActive(true);
    }
    public bool IsDialogueActive() // Method to check if dialogue is active
    {
        return startDialogue;
    }

    public IEnumerator TriggerEvent()
    {
        startDialogue = true;
        eventTriggered = true;
        inEvent = true;
        DeactivateUI();
        //Event Warung Owner scream
        if(currentDay == 2)
        {
            FindObjectOfType<AudioManager>().Play("Warung Owner scream");
            Quest quest2Part4 = new Quest
            (
                "Investigate what happened at the food stall",
                new List<Task>
                {
                    new Task("Talk to the owner of the food stall")
                }
            );

            questManager.AddQuest(quest2Part4);
        }

        yield return new WaitForSeconds(3f);

        dialogueTrigger22.SetActive(true);

        startDialogue = false;
        eventTriggered = false;
    }

    public void JumpscareEvent()
    {
        // If the jumpscare event has started
        if (dialogueManager.currSentence == "I NEED TO QUICKLY NAIL THIS QUILL INTO MY DOOR, OR ELSE..." && !dialogueManager.inDialogue && !inJumpscareEvent)
        {
            // Initialize jumpscare event
            jumpscareTime = day3JumpscareCooldown;

            // Set animator parameters for descending Kuyang
            animatorKuyang.SetLayerWeight(1, 1f);
            animatorKuyang.SetBool("Descending", true);
            animatorKuyang.SetFloat("Descending Speed", day3FloatSpeed);

            inJumpscareEvent = true;
            startCountdown = true;

            // Add quest for nailing the quill
            Quest quest3Part3 = new Quest
            (
                "Escape the Demon",
                new List<Task>
                {
                    new Task("Nail the quill to your house's front door with a hammer")
                }
            );

            questManager.AddQuest(quest3Part3);
        }else if(currentDay == 5)
        {
            Debug.Log("DAY 5 JUMPSCARE EVENT");
            // Initialize jumpscare event
            jumpscareTime = day5JumpscareCooldown;

            // Set animator parameters for descending Kuyang
            animatorKuyang.SetLayerWeight(1, 1f);
            animatorKuyang.SetBool("Descending", true);
            animatorKuyang.SetFloat("Descending Speed", day5FloatSpeed);

            inJumpscareEvent = true;
            startCountdown = true;

        }

        // Handle active jumpscare event
        if (inJumpscareEvent)
        {
            // Countdown logic
            if (startCountdown)
            {
                jumpscareTime -= Time.deltaTime;

                // Trigger jumpscare if time runs out
                if (jumpscareTime <= 0)
                {
                    Debug.Log(" ing Jumpscare!");
                    startCountdown = false; // Stop the countdown
                    StartCoroutine(TriggerJumpscare());
                    return;
                }
            }

            // Check if quill is nailed to the door
            if (quill.activeInHierarchy && quillHammered && !day3Survive && currentDay == 3)
            {
                Debug.Log("Quill nailed successfully!");
                dialogueTrigger35.SetActive(true);
                inJumpscareEvent = false; // End the jumpscare event
                startCountdown = false;  // Stop the countdown
                day3Survive = true;
                ResetKuyangAnimation();
            }

            if(currentDay == 5 && kuyangSetOnFire && !day5Survive)
            {
                inJumpscareEvent = false; // End the jumpscare event
                startCountdown = false;  // Stop the countdown
                
                ResetKuyangAnimation();
            }
        }   
    }

    private void ResetKuyangAnimation()
    {
        animatorKuyang.SetBool("Descending", false);
        animatorKuyang.SetFloat("Descending Speed", 0);
        animatorKuyang.SetLayerWeight(1, 0f); // Reset layer weight
    }
    public IEnumerator TriggerJumpscare()
    {
        Debug.Log("JUMPSCARE!!!!");
        startDialogue = true;
        DeactivateUI();
        kuyangJumpscare.SetActive(true);
        kuyangHover.SetActive(false);
        yield return new WaitForSeconds(3f);

        anim = screenPanel.GetComponent<Animator>();
        anim.SetBool("FadeIn", true);

        yield return new WaitForSeconds(3f);
        anim.SetBool("FadeIn", false);

        // Get the current active scene and reload it
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void UpdateAllDisplays()
    {
        for (int i = 0; i < numberObjects.Length; i++)
        {
            UpdateDisplay(i);
        }
    }

    private void UpdateDisplay(int index)
    {
        if (index >= 0 && index < numberObjects.Length)
        {
            numberObjects[index].text = numbers[index].ToString();
        }
    }

    public void IncrementNumber(int index)
    {
        if (index >= 0 && index < numbers.Length)
        {
            numbers[index]++;
            UpdateDisplay(index);
        }
    }

    public void DecrementNumber(int index)
    {
        if (index >= 0 && index < numbers.Length)
        {
            numbers[index]--;
            UpdateDisplay(index);
        }
    }

    public void CheckSafeLock()
    {
        if(numbers[0] == 3 && numbers[1] == 8 && numbers[2] == 9)
        {
            unlocked = true;
        }
    }

    public IEnumerator UnlockSafe()
    {
        Transform safeChild = safePanel.transform.GetChild(0);
        safeChild.GetComponent<Image>().sprite = lockOpen;
        FindObjectOfType<AudioManager>().Play("Safe Unlocked");

        yield return new WaitForSeconds(0.5f);

        //ActivateUI();
        safePanel.SetActive(false);

        safe.GetComponent<BoxCollider2D>().enabled = false;
        safe.GetComponent<SpriteRenderer>().enabled = false;

        Transform child = safe.transform.GetChild(0);
        child.gameObject.SetActive(true);

    }

    public IEnumerator PlayCutscene()
    {
        if (videoCanvas != null)
        {
            videoCanvas.SetActive(true); // Activate the canvas or RawImage
        }
       
        TimeManager.Instance.ShiftTime("Night");
        
        videoPlayer.Play(); // Start the video
        yield return new WaitForSeconds((float)videoPlayer.length);

        videoPlayer.Stop();
        videoCanvas.SetActive(false);

        //Post-cutscene changes
        chiefDead.SetActive(true);
        chiefKeyOnScene.SetActive(true);
        foodStallNPCs.SetActive(false);
        warungOwner.SetActive(false);
        ChiefInFoodStall.SetActive(false);
        // Execute your code after the video finishes
       
        
        JumpscareEvent();
    }

    public IEnumerator EndingCutscene()
    {
        if (videoCanvas2 != null)
        {
            videoCanvas2.SetActive(true); // Activate the canvas or RawImage
        }

        
        videoPlayer2.Play(); // Start the video

        yield return new WaitForSeconds(25f);

        for (int i = 0; i < endDialogue.Length; i++)
        {
            // Instantiate the prefab first
            GameObject newStartDialogue = Instantiate(startDialoguePrefab, startDialogueTransform);

            // Set the text of the instantiated object
            var textComponent = newStartDialogue.GetComponent<TMPro.TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = endDialogue[i];
            }

            // Add to the array
            startingDialogues[i] = newStartDialogue;
            startingDialogues[i].SetActive(false);

            yield return new WaitForSeconds(8f);
        }

        yield return new WaitForSeconds((float)videoPlayer2.length);
        anim.SetBool("FadeIn", true);
        videoPlayer2.Stop();
        videoCanvas2.SetActive(false);

        
    }
    public void HandleDay1()
    {
        if(!day1InterrogationTarget)
        {
            InterrogationManager.Instance.currentPanel = interrogationPanelPoorMan;
        }
         if(!day1StartDialogue)
            {
                DayStartDialogue();
                day1StartDialogue = true;
            }

            //Beginning Dialogue in Day 1
            if(!startDialogue) dialogueTrigger1.SetActive(true);

            if(ladder != null)
            ladder.GetComponent<BoxCollider2D>().enabled = false;

            //Once we enter Ananda's zone
            if (navigationManager.cameras[3].gameObject.activeInHierarchy)
            {
                if(!dialogueAnandaBool)
                {   
                    dialogueManager.StartDialogue(dialogueAnanda);
                    questManager.CompleteTask(1);
                    dialogueAnandaBool = true;
                }
                
            }

            //Once we enter abandoned house
            if(navigationManager.cameras[8].gameObject.activeInHierarchy)
            {
                dialogueTrigger11.SetActive(true);
            }

            //Once we enter Chief's house
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
            if (dialogueManager.currSentence == "Here are the matches I promised you earlier.")
            {
                if (!matchesTaken)
                {
                    inventoryManager.AddItem(Matches);
                    matchesTaken = true;
                }
                       
            }
    }

    public void HandleDay2()
    {
        PlayerPrefs.SetInt("CurrentDay", 2);
        PlayerPrefs.Save();

        if(!day3InterrogationTarget)
        {
            InterrogationManager.Instance.currentPanel = interrogationPanelPoorMan;
        }
        if(!day2ChangedTime)
        {
            TimeManager.Instance.ShiftTime("Noon");
            day2ChangedTime = true;
        }

        if(!day2StartDialogue && !inTransition)
        {
            DayStartDialogue();
            day2StartDialogue = true;
        }

        if(!startDialogue) dialogueTrigger2.SetActive(true);
        
        //Deactivate and activateObjects
        foodStallNPCs.SetActive(false);
        abandonedHouseDoor.SetActive(false);
        if(porcupine != null && porcupine2 != null)
        {
            porcupine.SetActive(true);
            porcupine2.SetActive(true);
        }
       
        // Beginning Quest in Day 2
        if (!Day2GivenQuest)
        {
            Quest quest2part1 = new Quest
            (
                "Visit the food stall to find information",
                new List<Task>
                {
                    new Task("Go to the food stall and ask anyone there")
                }
            );
            questManager.AddQuest(quest2part1);
            Day2GivenQuest = true;
        }

        if (dialogueManager.currSentence == "Here….. use this glove to get the quill.")
        {
            if (!glovesTaken)
            {
                inventoryManager.AddItem(Gloves);
                glovesTaken = true;
            }
                    
        }

            if (navigationManager.cameras[3].gameObject.activeInHierarchy)
        {
            dialogueTrigger25.SetActive(true);
        }
        //Check to see if player has 2 quills or not
        if(Quills.count == 2)
        {
            Debug.Log("IHAVE 2 QUILLS");
            if(questManager.quests[0].questName == "Track porcupines in the forest")
            {
                questManager.CompleteTask(0);

                Quest quest2Part3 = new Quest
                (
                    "Go back to the food stall",
                    new List<Task>
                    {
                        new Task("Give one quill to the food stall owner")
                    }
                );

                questManager.AddQuest(quest2Part3);
            }
        }

        if(dialogueManager.currSentence == "Thank you so much! I hope no one will be a victim of the Kuyang this time around. I am hopeful that your investigation will put an end to this meaningless bloodshed.")
        {
            hasGivenQuill = true;
        }
        if(hasGivenQuill && !dialogueManager.inDialogue)
        {
            dialogueTrigger21.SetActive(true);
        }

        //Trigger Event
        if(hasGivenQuill && navigationManager.cameras[1].gameObject.activeInHierarchy && !warungOwnerScream)
        {
            StartCoroutine(TriggerEvent());
            warungOwnerScream = true;
        }

        if(navigationManager.cameras[0].gameObject.activeInHierarchy && inEvent && !quillStolen)
        {
            dialogueTrigger26.SetActive(true);
            quillStolen = true;
        }
        
        if(navigationManager.cameras[8].gameObject.activeInHierarchy && !enteredAbandonedHouse && poorMan.activeInHierarchy)
        {
            dialogueTrigger23.SetActive(true);
            enteredAbandonedHouse = true;
            questManager.CompleteTask(0);

            Quest quest2Part6 = new Quest
            (
                "Confront the poor man in the abandoned house",

                new List<Task>
                {
                    new Task("Talk to the poor man"),
                    new Task("Confront the poor man about the theft")
                }
            );

            questManager.AddQuest(quest2Part6);
        }

        //Interrogation Aftermath
        if(InterrogationManager.Instance.interrogationCompleted && navigationManager.cameras[4].gameObject.activeInHierarchy && !postInterrogation)
        {
            Quest quest2Part7 = new Quest
            (
                "Go back to food stall",

                new List<Task>
                {
                    new Task("Give up your quill to the food stall owner")
                }
            );
            questManager.AddQuest(quest2Part7);
            dialogueTrigger24.SetActive(true);
            postInterrogation = true;
        }

        if(questManager.quests.Count > 0)
        //After talking to Warung Owner post-theft
        if(questManager.quests[0].questName == "Go back to food stall" && talkedPostInterrogation && !dialogueManager.inDialogue)
        {
            Debug.Log("Function called");
            questManager.CompleteTask(0);
            Quest quest2Part8 = new Quest
            (
                "Go back to your house.",

                new List<Task>
                {
                    new Task("Go home")
                }
            );
            questManager.AddQuest(quest2Part8);
            endDialogueDay2 = true;

            TimeManager.Instance.ShiftTime("Afternoon");
            
        }
    }

    public void HandleDay3()
    {
        PlayerPrefs.SetInt("CurrentDay", 3);
        PlayerPrefs.Save();

        if(!day3StartDialogue && !inTransition)
        {
            DayStartDialogue();
            day3StartDialogue = true;
        }

        if(!startDialogue) dialogueTrigger3.SetActive(true);

        //Deactivate objects
        abandonedHouseDoor.SetActive(true);
        quillPoorMan.SetActive(true);
       

        if(!Day3GivenQuest)
        {
            Quest quest3Part1 = new Quest
            (
                "Search for a porcupine",

                new List<Task>
                {
                    new Task("Find a quill as soon as possible")
                }
            );
            questManager.AddQuest(quest3Part1);
            Day3GivenQuest = true;
        }
        
        //When we visit the poor man on the 3rd day
        if(navigationManager.cameras[8].gameObject.activeInHierarchy && !Day3GivenQuest2)
        {
            questManager.CompleteTask(0);
            dialogueTrigger31.SetActive(true);

            Quest quest3Part2 = new Quest
            (
                "Bring water to the poor man",

                new List<Task>
                {
                    new Task("Find a water source."),
                    new Task("Try to obtain water"),
                    new Task("Bring water to the poor man")
                }
            );
            questManager.AddQuest(quest3Part2);

            Day3GivenQuest2 = true;
        }

        //When we visit the well on the 3rd day
        if(navigationManager.cameras[7].gameObject.activeInHierarchy && questManager.quests[0].questName == "Bring water to the poor man")
        {
            questManager.CompleteTask(0);
            dialogueTrigger32.SetActive(true);
        }

        if(dialogueManager.currSentence == "Here, take this axe and chop down the marked tree." && !axeTaken)
        {
            inventoryManager.AddItem(Axe);
            axeTaken = true;
        }

        if(dialogueManager.currSentence == "Here you go." && !pulleyTaken)
        {
            inventoryManager.AddItem(Pulley);
            pulleyTaken = true;
        }

        if(Inventory.instance.HasItem("Wood") && !axeTaken2)
        {
            inventoryManager.AddItem(Axe);
            axeTaken2 = true;
        }

        //Well Logic
        SpriteRenderer bucketSprite = BucketSpot.GetComponent<SpriteRenderer>();
        if(objectAction == 1)
        {
            bucketSprite.sprite = wellDown;
        }else if(objectAction == 2)
        {
            bucketSprite.sprite = wellUp;
            BucketSpot.GetComponent<BoxCollider2D>().enabled = true;
        }

        //Jumpscare event after putting water bucket in front of abandoned house
        if(waterBucket.activeInHierarchy && !waterBucketGiven)
        {
            inventoryManager.AddItem(Quills);
            Debug.Log("Function Called");

            dialogueTrigger33.SetActive(true);
            waterBucketGiven = true;
        }
        
        if(dialogueManager.currSentence == "Thank you once again, kind sir." && !jumpscareStarted && !dialogueManager.inDialogue)
        {
            ghostEyes.SetActive(true);
            dialogueTrigger34.SetActive(true);
            jumpscareStarted = true;
            TimeManager.Instance.ShiftTime("Night");
        }

        JumpscareEvent();

        if(dialogueManager.currSentence == "I’m safe….. Thank God" && !dialogueManager.inDialogue && !day3Transition)
        {
            StartCoroutine(DayTransition());
            day3Transition = true;
        }
    }

    public void HandleDay4()
    {
        PlayerPrefs.SetInt("CurrentDay", 4);
        PlayerPrefs.Save();
        if(!day4ChangedTime)
        {
            TimeManager.Instance.ShiftTime("Noon");
            day4ChangedTime = true;
        }
        if(!day4StartDialogue && !inTransition)
        {
            DayStartDialogue();
            day4StartDialogue = true;
        }

        //Beginning Dialogue in Day 4
        if(!startDialogue) dialogueTrigger4.SetActive(true);

        Ananda.SetActive(true);
        Chief.SetActive(false);

        quill.SetActive(false);
        if(shovelObject != null) {shovelObject.SetActive(true);}
        flowersContainer.SetActive(true);
        notActivated = false;
        if(!Day4GivenQuest)
        {
            Quest quest4Part1 = new Quest
            (
                "Explore the village",

                new List<Task>
                {
                    new Task("Look around the village for more clues")
                }
            );

            questManager.AddQuest(quest4Part1);
            Day4GivenQuest = true;
        }
        
        //Once we enter Ananda's house area
        if(navigationManager.cameras[3].gameObject.activeInHierarchy && !anandaGivenQuest)
        {
            dialogueTrigger41.SetActive(true);
            questManager.CompleteTask(0);

            Quest quest4part2 = new Quest
            (
                "Comfort Ananda with flowers",

                new List<Task>
                {
                    new Task("Gather 10 flowers to create a bouquet"),
                    new Task("Give the bouquet to Ananda")
                }
            );

            questManager.AddQuest(quest4part2);
            anandaGivenQuest = true;
        }
        if(flowers.count == 10)
        {
            inventoryManager.RemoveItem(flowers, 10);
            inventoryManager.AddItem(bouquet);
            hasBouquet = true;
        }
        //Check for Jasmine flowers count
        if(questManager.quests.Count > 0)
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
        

        //Activate kuyang Hair if player has shovel
        if (Inventory.instance.HasItem("Shovel") && !shovelTaken)
        {
            if(questManager.quests[0].questName == "Go back to your house.")
            {
                if (kuyangHair != null)
                    kuyangHair.SetActive(true);

                if (tornClothObject != null)
                    tornClothObject.SetActive(true);
                
                TimeManager.Instance.ShiftTime("Afternoon");
                shovelTaken = true;
            }
            else if (questManager.quests[0].questName == "Investigate traces of the mess from the graveyard.")
            {
                questManager.CompleteTask(0);

                Quest quest4part6 = new Quest
                (
                    "Go back to your house.",

                    new List<Task>
                    {
                        new Task("Call it a day and rest for tomorrow")
                    }
                );

                questManager.AddQuest(quest4part6);
            }
        }

        if(Inventory.instance.HasItem("Shovel") && !shovelTaken2)
        {
             dialogueManager.StartDialogue(dialogueShovel);
             shovelTaken2 = true;
        }
        if(kuyangHair != null && tornClothObject != null)
        if(navigationManager.cameras[1].gameObject.activeInHierarchy && kuyangHair.activeSelf && tornClothObject.activeSelf && !Day4GivenQuest2)
        {
            dialogueTrigger43.SetActive(true);
            questManager.CompleteTask(0);
            mcDoor.GetComponent<BoxCollider2D>().enabled = false;
            Quest quest4part7 = new Quest
            (
                "Investigate the front of your house",

                new List<Task>
                {
                    new Task("Investigate the object pinned to the door"),
                    new Task("Investigate the piece of fabric on the fence")
                }
            );

            questManager.AddQuest(quest4part7);
            Day4GivenQuest2 = true;
        }
        if(Inventory.instance.HasItem("Kuyang Hair") && !dialogueKuyangHairBool)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueKuyangHair);
            dialogueKuyangHairBool = true;
            questManager.CompleteTask(0);
        }

        if(Inventory.instance.HasItem("Torn Cloth") && !dialogueTornClothBool)
        {
            FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueTornCloth);
            dialogueTornClothBool = true;
            questManager.CompleteTask(1);
        }
        if(dialogueKuyangHairBool && dialogueTornClothBool && !dialogueManager.inDialogue)
        {
            StartCoroutine(DayTransition());
        }
    }

    public void HandleDay5()
    {
        PlayerPrefs.SetInt("CurrentDay", 5);
        PlayerPrefs.Save();
        Item selectedItem = inventoryManager.GetSelectedItem();

        if(!day5InterrogationTarget)
        {
            Debug.Log("THIS IS THE TARGET");
            InterrogationManager.Instance.currentPanel = interrogationPanelChief;
            day5InterrogationTarget = true;
        }
        
        if(!day5ChangedTime)
        {
            TimeManager.Instance.ShiftTime("Afternoon");
            day5ChangedTime = true;
        }
        if(!day5StartDialogue && !inTransition)
        {
            DayStartDialogue();
            day5StartDialogue = true;
        }

        //Disable NPCs
        Chief.SetActive(false);
        Agus.SetActive(false);

        //Activate CertainObjects
        if(ladder != null)
        Debug.Log("GOTCHA");
        ladder.GetComponent<BoxCollider2D>().enabled = true;

        //Beginning Dialogue in Day 5
        if(!startDialogue) dialogueTrigger5.SetActive(true);
        
        // Beginning Quest in Day 5
        if (!Day5GivenQuest)
        {
            Quest quest5part1 = new Quest
            (
                "Find a way to get inside the Chief's house",
                new List<Task>
                {
                    new Task("Do whatever it takes to get inside his house")
                }
            );
            questManager.AddQuest(quest5part1);
            Day5GivenQuest = true;
        }

        //Once we enter Chief's house
        if (navigationManager.cameras[6].gameObject.activeInHierarchy)
        {
            if(!dialogueChiefDay5Bool)
            {   
                dialogueManager.StartDialogue(dialogueChiefDay5);
                dialogueChiefDay5Bool = true;
            }
        }     

        //Once we enter Agus' house
        if(navigationManager.cameras[5].gameObject.activeInHierarchy)
        {
            if(currentDay == 5)
            {
                if(!dialogueAgusDay5Bool)
                {   
                    dialogueManager.StartDialogue(dialogueAgusDay5);
                    dialogueAgusDay5Bool = true;
                }   
                
            }
        }
        
        if(selectedItem != null)
        {
            if (selectedItem.itemName == "Note Clue" && !noteSelected && !noteCluePanel.activeInHierarchy)
            {
                DeactivateUI();
                noteCluePanel.SetActive(true);
                noteSelected = true;    
            }else if(selectedItem.itemName == "Confession Note" && !confessionNotePanel.activeInHierarchy && !seenConfessionNote)
            {
                DeactivateUI();
                confessionNotePanel.SetActive(true);
                noteSelected = true; 
                seenConfessionNote = true;
                if(questManager.quests.Count > 0)
                questManager.CompleteTask(0);
            }
        }
       
        
        // Check for deactivating panels
        if ((noteCluePanel.activeSelf || confessionNotePanel.activeSelf) && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateUI();
            if(selectedItem != null)
            if(selectedItem.itemName == "Confession Note")
            {
                dialogueTrigger52.SetActive(true);
                ChiefInFoodStall.SetActive(true);
                foodStallNPCs.SetActive(true);
            }
            // Deactivate all panels
            noteCluePanel.SetActive(false);
            confessionNotePanel.SetActive(false);
            safePanel.SetActive(false);

            // Reset selection
            noteSelected = false;
            inventoryManager.DeselectItem();
        }

        if(safePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateUI();

            safePanel.SetActive(false);

            lockSelected = false;
        }
        //Check Completion 
        CheckSafeLock();

        //Safe Completion
        if(unlocked)
        {
            StartCoroutine(UnlockSafe());
        }

        //Chief confrontation quest
        if(dialogueManager.currSentence == "I need to talk to him about this." && !dialogueManager.inDialogue && !Day5GivenQuest2)
        {
            Quest quest5part3 = new Quest
            (
                "Confront the Chief",

                new List<Task>
                {
                    new Task("Expose the Chief's involvement in the recent murders")
                }
            );

            questManager.AddQuest(quest5part3);
            dialogueTrigger52.SetActive(true);
            warungDoor.SetActive(false);

            Day5GivenQuest2 = true;
        }

        if(dialogueManager.currSentence == "Exorcise the demon for the sake of the village—and for everyone." && !dialogueManager.inDialogue && !cutscene1)
        {
            // Start the cutscene
            StartCoroutine(PlayCutscene());

            // Set the flag to prevent re-triggering
            cutscene1 = true;
        }

        if(cutscene1 && !Day5GivenQuest3)
        {
            Quest quest5part4 = new Quest
            (
                "Exorcise the Kuyang",
                new List<Task>
                {
                    new Task("Find the Kuyang's body and kill it for good")
                }
            );

            // Add the new quest
            questManager.AddQuest(quest5part4);
            Day5GivenQuest3 = true;
        }
      
    }
}