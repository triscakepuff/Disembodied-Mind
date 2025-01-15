using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestManager : MonoBehaviour
{
    public List<Quest> quests; // List of quests
    public TextMeshProUGUI questNameText; // UI element for quest name
    public Transform taskListContainer; // Parent object for task UI elements
    public GameObject taskItemPrefab; // Prefab for task UI elements
    public Quest currentQuest;

    [Header("Managers")]

    public bool hasActiveQuest = false;
        void Start()
    {
        quests = new List<Quest>();
        Quest();
    }

    void Update()
    {
        //Debug.Log(quests[0].questName);
    }

    public void AddQuest(Quest newQuest)
    {
         Debug.Log("Added Quest: " + currentQuest.questName);
        quests.Add(newQuest);
        if (!hasActiveQuest)
        {
            DisplayNextQuest();
        }
    }

    public void DisplayNextQuest()
    {
        if (quests.Count == 0)
        {
            EndQuest();
            return;
        }

        hasActiveQuest = true;
        currentQuest = quests[0];
        questNameText.text = currentQuest.questName;

        // Clear existing task UI
        foreach (Transform child in taskListContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate task UI
        foreach (Task task in currentQuest.tasks)
        {
            GameObject taskItem = Instantiate(taskItemPrefab, taskListContainer);
            TextMeshProUGUI taskDescription = taskItem.GetComponentInChildren<TextMeshProUGUI>();
            Image completionBox = taskItem.transform.Find("Completion").GetComponent<Image>();

            taskDescription.text = task.taskDescription;
            completionBox.color = task.isCompleted ? Color.green : Color.red;
        }
    }

    public void CompleteTask(int taskIndex)
    {
        Debug.Log("TASK DONE");
        if (currentQuest == null || taskIndex < 0 || taskIndex >= currentQuest.tasks.Count)
        {    
            Debug.LogError("Invalid taskIndex: " + taskIndex);
            return;
        }

        Task task = currentQuest.tasks[taskIndex];
        task.isCompleted = true;

        // Update task UI
        Transform taskItem = taskListContainer.GetChild(taskIndex);
        Image completionBox = taskItem.transform.Find("Completion").GetComponent<Image>();
        completionBox.color = Color.green;

        // Check if all tasks are completed
        if (currentQuest.tasks.TrueForAll(t => t.isCompleted))
        {
            CompleteCurrentQuest();
        }
    }

    public void CompleteCurrentQuest()
    {
        Debug.Log("Completing Quest: " + currentQuest.questName);
        quests.RemoveAt(0);

        if (quests.Count > 0)
        {
            DisplayNextQuest();
        }
        else
        {
            EndQuest();
        }
    }

    public Quest FindQuestByName(string questName)
    {
        return quests.Find(q => q.questName == questName);
    }
void EndQuest()
{
    hasActiveQuest = false;
}

void Quest()
{
        Quest quest1 = new Quest
        (
            "Meet the Neighbours",
            new List<Task>
            {
                 new Task("Meet the Chief at his house."),
                 new Task("Go to Ananda's house and talk to her."),
                 new Task("Talk to Agus at his house.")
            }
        );

        AddQuest(quest1);

        Quest quest2Part1 = new Quest
        (
            "Meet the Owner of the food stall",
            new List<Task>
            {
                 new Task("Speak to owner of the food stall"),
            }
        );

        AddQuest(quest2Part1);

        Quest quest2Part2 = new Quest
        (
            "Gather items for the ritual",
            new List<Task>
            {
                 new Task("Obtain oil"),
                 new Task("Obtain matches.")
            }
        );

        AddQuest(quest2Part2);

        Quest quest2Part3 = new Quest
    (
        "Go back to your house.",
        new List<Task>
        {
            new Task("Go home")
        }
    );

    AddQuest(quest2Part3);
   
}

}
