using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrogationManager : MonoBehaviour
{
    public static InterrogationManager Instance;
    public DialogueManager dialogueManager;
    public GameObject interrogationPanel;
    public GameObject interrogationPanelPoorMan;
    public GameObject interrogationPanelChief;

    public GameObject ButtonPoorMan1;
    private bool poorManInterrogationDone = false;

    public bool interrogationPanelActive = false;
    public bool interrogationCompleted = false;
    public bool inInterrogation = false;
    private bool dialogueTriggered = false;

    public GameObject currentPanel;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            interrogationPanel.SetActive(false);
        }

        if(interrogationPanel.activeInHierarchy)
        {
            interrogationPanelActive = true;
        }else
        {
            interrogationPanelActive = false;
        }

        if(GameManager.Instance.dialogueManager.inDialogue)
        {
            currentPanel.SetActive(false);
        }else if(!GameManager.Instance.dialogueManager.inDialogue && !interrogationCompleted)
        {
            currentPanel.SetActive(true);
        }
        if(CheckAllObjections() && !dialogueTriggered)
        {
            Debug.Log("TRIGGERING DIALOGUE");
            TriggerDialogueForPanel();
            dialogueTriggered = true;
            interrogationPanel.SetActive(false);
        }
    }

    public bool CheckAllObjections()
    {
        if (currentPanel == null) return false;

        // Iterate through the child objects of the current panel
        foreach (Transform child in currentPanel.transform)
        {
            Interrogation interrogationBool = child.GetComponent<Interrogation>();
            if (interrogationBool != null && !interrogationBool.solved)
            {
                Debug.Log("Objection not solved: " + child.gameObject.name);
                return false; // Exit early if any objection is not solved
            }
        }

        Debug.Log("All objections solved for panel: " + currentPanel.name);
        interrogationCompleted = true;
        return true; // All objections are solved
    
    }

    private void TriggerDialogueForPanel()
    {
        Debug.Log("WOWOOWOWOW");
        if (currentPanel == interrogationPanelPoorMan)
        {
            FindAnyObjectByType<QuestManager>().CompleteTask(1);
            dialogueManager.StartDialogue(GameManager.Instance.dialoguePoorManFinished);
        }
        else if (currentPanel == interrogationPanelChief)
        {
            dialogueManager.StartDialogue(GameManager.Instance.dialogueChiefFinished);
        }
    // }
    // public void AfterInterrogation()
    // {
    //     Debug.Log("Function Called");
    //     FindAnyObjectByType<QuestManager>().CompleteTask(1);
    //     interrogationPanel.SetActive(false);
    //     interrogationCompleted = true;
    //     inInterrogation = false;
    // }
    }
}
