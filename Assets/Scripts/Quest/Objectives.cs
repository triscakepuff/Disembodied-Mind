using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Task
{
    public string taskDescription; // Description of the task
    public bool isCompleted; // Completion status of the task

    public Task(string taskDescription)
    {
        this.taskDescription = taskDescription;
        isCompleted = false; // Default to not completed
    }
}