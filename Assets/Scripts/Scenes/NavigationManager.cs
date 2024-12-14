using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public Camera[] cameras;
    public GameObject[] navigationUI;
    public GameObject dialogueScreen;

    [Header("Managers")]
    public static NavigationManager Instance;
    public DialogueManager dialogueManager;
    public QuestManager questManager;

    void Start()
    {
    }

    void Update()
    {
        UpdateNavigationUI();
        Quest1Condition();
    }

    void UpdateNavigationUI()
    {
        // Check if startingDialogue is true
        if (dialogueManager.inDialogue || GameManager.Instance.inTransition || GameManager.Instance.startDialogue)
        {
            // Disable all navigation UI elements if startingDialogue is true
            foreach (var ui in navigationUI)
            {
                ui.SetActive(false);
            }
            return; // Exit the method early
        }else
        {
             // Enable/disable navigation UI based on camera visibility
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i].gameObject.activeInHierarchy)
                {
                    navigationUI[i].SetActive(true);
                    dialogueScreen.transform.position = cameras[i].transform.position;
                    dialogueScreen.transform.rotation = cameras[i].transform.rotation;
                }
                else
                {
                    navigationUI[i].SetActive(false);
                }
            }
        }

       
    }

    public void OnDialogueEnd()
    {
        // This method will be called when the dialogue ends
        UpdateNavigationUI(); // Re-evaluate the navigation UI state
    }

    public void Quest1Condition()
    {
        Transform foodStall = navigationUI[1].transform.Find("Food");
        Transform graveyard = navigationUI[1].transform.Find("Graveyard");
        if(questManager.quests[0].questName == "Meet the Neighbours")
        {
            foodStall.gameObject.SetActive(false);
            graveyard.gameObject.SetActive(false);
        }else
        {
            foodStall.gameObject.SetActive(true);
            graveyard.gameObject.SetActive(true);
        }
    }
}