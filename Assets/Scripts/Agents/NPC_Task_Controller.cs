using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

public class NPC_Task_Controller : MonoBehaviour
{
    public List<NPCTask> tasks = new List<NPCTask>();
    private int[] task_order;
    public NPCTask activeTask;

    private int TaskCounter = 0;

    private bool restingAfterDone = false;
    public float restingTimeBetweenTasks = 5f;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        task_order = Enumerable.Range(0, tasks.Count).ToArray();
        task_order = shuffleArray(task_order);
        activeTask = tasks[task_order[TaskCounter % tasks.Count]];
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTask.done && !restingAfterDone)
        {
            restingAfterDone = true;
            Invoke("SetNextTask", restingTimeBetweenTasks);
        }

    }

    private void SetNextTask()
    {
        TaskCounter += 1;
        activeTask = tasks[task_order[TaskCounter % tasks.Count]];
        if (TaskCounter % tasks.Count == 0)
            RestTaskList();
        CancelInvoke("SetNextTask");
        restingAfterDone = false;
    }
    
    public NPCTask currentTask()
    {
        return activeTask;
    }

    private void RestTaskList()
    {
        foreach (var task in tasks)
        {
            task.done = false;
        }
        task_order = shuffleArray(task_order);
    }

    //Randomly shuffle a List (taken from https://stackoverflow.com/questions/273313/randomize-a-listt)
    private static int[] shuffleArray(int[] arr)  
    {
        Random rand = new Random();  
        return arr.OrderBy(a => rand.Next()).ToArray();
    }
}
