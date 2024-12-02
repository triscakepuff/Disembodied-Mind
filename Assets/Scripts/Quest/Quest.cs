using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public string questName;
     public List<Task> tasks; // List of tasks
    public bool isCompleted;
    public bool isActive;

    // Constructor
    public Quest(string questName,  List<Task> tasks)
    {
        this.questName = questName;
        this.tasks = tasks;
        this.isActive = true;
    }
}

