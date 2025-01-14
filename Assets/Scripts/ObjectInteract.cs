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
    private BoxCollider2D boxCollider2D;
    private bool placedLadder = false;
    private bool doorUnlocked = false;
    private bool hasTakenOil = false;
    private bool canbeOpened = false;
    private bool safeSelected = false;
    private bool unlocked = false;
    public QuestManager questManager;
    void Start()
    {
        if(gameObject.name == "Cupboard" || gameObject.name == "Laci" || gameObject.name == "Cupboard 1" || gameObject.name == "Cupboard 2" || gameObject.name == "Laci 1" || gameObject.name == "Laci 2") canbeOpened = true;
        if(dialogueLine != null)
        {
            dialogueAnimator = dialogueLine.GetComponent<Animator>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D  = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnMouseDown()
    {
        Debug.Log("Button Pressed");
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
                FindObjectOfType<AudioManager>().Play("Opening Safe Lock");
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
                Quest quest4part4 = new Quest
                    (
                        "Go back to your house.",
                        new List<Task>
                        {
                            new Task("Go home.")
                        }
                    );

                questManager.AddQuest(quest4part4);
            }

            if(SelectedItem.itemName == "Ladder" && gameObject.name == "Ladder Area")
            {
                GameManager.Instance.placedLadder = true;
                Inventory.instance.DeselectItem();
                Inventory.instance.RemoveItem(SelectedItem, 1);
                questManager.CompleteTask(0);
                spriteRenderer.enabled = true;
                Quest quest5part2 = new Quest
                    (
                        "Explore the Chief's house",
                        new List<Task>
                        {
                            new Task("Search for incriminating evidence to confront the Chief")
                        }
                    );

                questManager.AddQuest(quest5part2);
            }

            if(SelectedItem.itemName == "Gloves" && (gameObject.name == "Porcupine" || gameObject.name == "Porcupine (1)"))
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.AddItem(GameManager.Instance.Quills);
                Destroy(gameObject);
               
            }

            if(SelectedItem.itemName == "Axe" && gameObject.name == "Tree Spot")
            {
                //Change sprite from whole tree to cut tree
                Transform child = gameObject.transform.GetChild(0);

                SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 2;
                Inventory.instance.AddItem(GameManager.Instance.Log);

                //Obtain Wood
                Inventory.instance.DeselectItem();
                
            }

            if(SelectedItem.itemName == "Pulley" && gameObject.name == "Pulley")
            {
                Inventory.instance.DeselectItem();
                spriteRenderer.enabled = true;
                Inventory.instance.RemoveItem(SelectedItem, 1);

                //New interact line
                interactLine = "The pulley now works. It's about time i fetch the poor man some water.";

            }

            if(SelectedItem.itemName == "Empty Bucket" && gameObject.name == "Bucket Spot")
            {
                Inventory.instance.DeselectItem();
                spriteRenderer.enabled = true;
                boxCollider2D.enabled = false;
                Inventory.instance.RemoveItem(SelectedItem, 1);
            }

            if(SelectedItem.itemName == "Filled Bucket" && gameObject.name == "Abandoned House Door")
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.RemoveItem(SelectedItem, 1);
                GameManager.Instance.waterBucket.SetActive(true);
                questManager.CompleteTask(0);
                questManager.CompleteTask(1);
                questManager.CompleteTask(2);
                Debug.Log("Bucket is active: " + GameManager.Instance.waterBucket.activeInHierarchy);
            }

            if(SelectedItem.itemName == "Quills" && gameObject.name == "MC's Door (1)" && GameManager.Instance.currentDay == 3)
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.RemoveItem(SelectedItem, 1);
                GameManager.Instance.quill.SetActive(true);
                boxCollider2D.enabled = false;
            }

            if(SelectedItem.itemName == "Hammer" && gameObject.name == "QuillOnScene")
            {
                Inventory.instance.DeselectItem();
                questManager.CompleteTask(0);
                GameManager.Instance.quillHammered = true;
            }

            if(SelectedItem.itemName == "Door Key" && gameObject.name == "Bedroom Door")
            {
                Inventory.instance.DeselectItem();
                FindObjectOfType<AudioManager>().Play("Unlock Door");
                Inventory.instance.RemoveItem(SelectedItem, 1);
                GameManager.Instance.doorUnlocked = true;
            }

            if(SelectedItem.itemName == "Chief's Key" && gameObject.name == "Blue Box")
            {
                Inventory.instance.DeselectItem();
                Transform child = gameObject.transform.GetChild(0);
                child.gameObject.SetActive(true);
                boxCollider2D.enabled = false;
            }

            if(SelectedItem.itemName == "Bolt Cutter" && gameObject.name == "Fence")
            {
                Inventory.instance.DeselectItem();
                spriteRenderer.enabled = false;
                Transform child = gameObject.transform.GetChild(0);
                child.gameObject.SetActive(true);
                GameManager.Instance.fenceCut = true;
            }

            if(SelectedItem.itemName == "Axe" && gameObject.name == "Hatch" && GameManager.Instance.fenceCut)
            {
                Inventory.instance.DeselectItem();
                Inventory.instance.RemoveItem(SelectedItem, 1);
                spriteRenderer.enabled = false;
                Transform child = gameObject.transform.GetChild(0);
                child.gameObject.SetActive(true);
                GameManager.Instance.hatchOpened = true;
            }

            if(SelectedItem.itemName == "Wooden Stakes" && gameObject.name == "Kuyang's Body")
            {
                Transform child = gameObject.transform.GetChild(1);
                Inventory.instance.RemoveItem(SelectedItem, 1);
                child.gameObject.SetActive(true);
                GameManager.Instance.putStake = true;
            }
            
            if(SelectedItem.itemName == "Jelangkung" && gameObject.name == "Kuyang's Body")
            {
                Inventory.instance.RemoveItem(SelectedItem, 1);
                Transform child = gameObject.transform.GetChild(2);
                child.gameObject.SetActive(true);
                GameManager.Instance.putJelangkung = true;
            }

            if(SelectedItem.itemName == "Filled Oil Can" && gameObject.name == "Kuyang's Body")
            {
                Inventory.instance.RemoveItem(SelectedItem, 1);
                Transform child = gameObject.transform.GetChild(0);
                child.gameObject.SetActive(true);
                GameManager.Instance.putOil = true;
            }

            if(SelectedItem.itemName == "Matches" && gameObject.name == "Kuyang's Body" && GameManager.Instance.putOil && GameManager.Instance.putJelangkung && GameManager.Instance.putStake)
            {
                Inventory.instance.RemoveItem(SelectedItem, 1);
                StartCoroutine(GameManager.Instance.EndingCutscene());
                GameManager.Instance.kuyangSetOnFire = true;
            } 
        }else
        {
            //Debug.Log(gameObject.name);
            //Debug.Log(questManager.quests[0].questName);
            if(questManager.quests.Count > 0)
            if(questManager.quests[0].questName == "Go back to your house." && (gameObject.name == "MC's Door" || gameObject.name == "MC's Door (1)"))
            {
                if(GameManager.Instance.currentDay == 4)
                {
                    if(!Inventory.instance.HasItem("Shovel"))
                    {
                        GameManager.Instance.dialogueTrigger42.SetActive(true);
                        questManager.CompleteTask(0);
                        Quest quest4part5 = new Quest
                        (
                            "Investigate traces of the mess from the graveyard.",

                            new List<Task>
                            {
                                new Task("Find evidence that could cause the mess")
                            }
                        );
                        questManager.AddQuest(quest4part5);
                        interactLine = "I need to find out who’s messing with the grave.";
                        dialogueLine.text = interactLine;
                    }else
                    {
                        questManager.CompleteTask(0);
                        StartCoroutine(GameManager.Instance.DayTransition());
                    }
                   
                }else
                {
                    questManager.CompleteTask(0);
                    StartCoroutine(GameManager.Instance.DayTransition());
                }
            }
            
          
            else if(Inventory.instance.HasItem("Shovel") && questManager.quests[0].questName == "Investigate traces of the mess from the graveyard.")
            {
                questManager.CompleteTask(0);
            }
            else if(gameObject.name == "Pulley")
            {
                if(GameManager.Instance.BucketSpot.GetComponent<SpriteRenderer>().enabled && GameManager.Instance.PulleyOnScene.GetComponent<SpriteRenderer>().enabled)
                {
                    GameManager.Instance.objectAction++;
                }
            }else if(gameObject.name == "Bucket Spot")
            {
                if(GameManager.Instance.objectAction == 2)
                {
                    Inventory.instance.AddItem(GameManager.Instance.FilledBucket);
                    Destroy(gameObject);
                    questManager.CompleteTask(1);
                }
            }else if(gameObject.name == "Window" && GameManager.Instance.placedLadder)
            {
                CameraController.Instance.SwitchToCamera(11);
                FindObjectOfType<AudioManager>().Play("Move");
    
            }else if(gameObject.name == "Bedroom Door" && GameManager.Instance.doorUnlocked)
            {
                CameraController.Instance.SwitchToCamera(13);
                FindObjectOfType<AudioManager>().Play("Move");
            }else if(gameObject.name == "Vase")
            {
                Transform child = gameObject.transform.GetChild(0);
                gameObject.transform.position = child.position;
            }else if(gameObject.name == "Safe")
            {
                unlocked = GameManager.Instance.unlocked;
                if(!unlocked && !GameManager.Instance.safePanel.activeInHierarchy && !GameManager.Instance.lockSelected)
                {
                    GameManager.Instance.DeactivateUI();
                    GameManager.Instance.safePanel.SetActive(true);
                    GameManager.Instance.lockSelected = true;
                }
                if(unlocked)
                {
                    boxCollider2D.enabled = false;
                    spriteRenderer.enabled = false;

                    Transform child = gameObject.transform.GetChild(0);
                    child.gameObject.SetActive(true);
                }
            }else if(gameObject.name == "Cloth")
            {
                GameManager.Instance.dialogueTrigger51.SetActive(true);
            }else if(gameObject.name == "Hatch" && GameManager.Instance.fenceCut && GameManager.Instance.hatchOpened)
            {
                CameraController.Instance.SwitchToCamera(15);
                FindObjectOfType<AudioManager>().Play("Move");
            }
            else if(canbeOpened)
            {
                boxCollider2D.enabled = false;
                spriteRenderer.enabled = false;

                Transform child = gameObject.transform.GetChild(0);
                SpriteRenderer spriteRendererChild = child.GetComponent<SpriteRenderer>();
                child.gameObject.SetActive(true);

                if(gameObject.name == "Cupboard 2")
                {
                   GameManager.Instance.clueNote.SetActive(true);
                }
            }
            else
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

